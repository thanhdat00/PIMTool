using System;
using System.Transactions;
using NHibernate;
using IsolationLevel = System.Data.IsolationLevel;

namespace PIMTool.Services.Service.Pattern
{

    public enum UnitOfWorkScopeOption
    {
        Required,
        RequiresNew
    }

    public class UnitOfWorkScope : IUnitOfWorkScope
    {

        #region SFields

        /// <summary>
        /// Field similar to <see cref="Transaction.Current"/> in oder to get
        /// access to the transaction's root scope.
        /// </summary>
        [ThreadStatic]
        private static UnitOfWorkScope s_currentScope;

        #endregion SFields

        #region IFields

        private bool m_complete;

        private bool m_nhibernateControlsTransaction = true;

        private readonly ISessionFactory m_factory;

        // Wrapped nhibernate session
        private ISession m_session;

        // Saved 'UnitOfWorkScope.Current', represents a kind of stack frame.
        private UnitOfWorkScope m_savedScope;

        #endregion IFields

        #region IConstructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkScope"/> class.
        /// </summary>
        /// <remarks>
        /// Uses <seeRequiredequired"/> default for
        /// UnitOfWorkScope inheritance.
        /// </remarks>
        public UnitOfWorkScope(ISessionFactory factory)
            : this(UnitOfWorkScopeOption.Required, factory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkScope"/> class. with the specified requirements.
        /// </summary>
        /// <param name="scopeOption">
        /// An instance of the <see cref="TransactionScopeOption"/> enumeration
        /// that describes the transaction requirements associated with this transaction scope.
        /// </param>
        /// <param name="factory"></param>
        public UnitOfWorkScope(UnitOfWorkScopeOption scopeOption, ISessionFactory factory)
            : this(scopeOption, IsolationLevel.ReadCommitted, factory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkScope"/> class. with the specified requirements.
        /// </summary>
        /// <param name="scopeOption">
        /// An instance of the <see cref="TransactionScopeOption"/> enumeration
        /// that describes the transaction requirements associated with this transaction scope.
        /// </param>
        /// <param name="transactionOptions">
        /// A <see cref="TransactionOptions"/> structure that describes the
        /// transaction options to use if a new transaction is created. If an existing transaction is
        /// used, the timeout value in this parameter applies to the transaction scope. If that time
        /// expires before the scope is disposed, the transaction is aborted.
        /// </param>
        /// <param name="factory"></param>
        public UnitOfWorkScope(UnitOfWorkScopeOption scopeOption,
            IsolationLevel transactionOptions, ISessionFactory factory)
        {
            m_factory = factory;
            InitializeScopeLinking(scopeOption, transactionOptions);
        }

        #endregion IConstructors

        #region SProperties

        /// <summary>
        /// Current property is read-only
        /// </summary>
        public static UnitOfWorkScope Current => s_currentScope;

        #endregion SProperties

        #region IProperties

        /// <summary>
        /// Provides access to the underlying <c>System.Transactions.TransactionScope</c>.
        /// </summary>
        public ISession Session => m_session;

        #endregion IProperties

        #region IMethods

        #region InitializeScope

        /// <summary>
        /// Initializes a new session (if not inherited from ambient
        /// transaction) and updates the linking to the root scope.
        /// </summary>
        private void InitializeScopeLinking(UnitOfWorkScopeOption scopeOption, IsolationLevel isolationLevel)
        {
            InitializeScope(IsRootUnitOfWorkScope() ? UnitOfWorkScopeOption.RequiresNew : scopeOption, isolationLevel);
        }

        /// <summary>
        /// Checks if this scope is a root unitofwork scope.
        /// </summary>
        private static bool IsRootUnitOfWorkScope()
        {
            return Current == null;
        }

        /// <summary>
        /// Initializes the current scope as a new child scope (= non root).
        /// </summary>
        private void InitializeScope(UnitOfWorkScopeOption scopeOption, IsolationLevel isolationLevel)
        {
            switch (scopeOption)
            {
                case UnitOfWorkScopeOption.Required:
                    // Inherit connection cache & keep current root scope
                    InheritSession();
                    PushScope(Current);
                    break;

                case UnitOfWorkScopeOption.RequiresNew:
                    // Create new connection cache & become new root scope
                    InitializeNewSession(isolationLevel);
                    PushScope(this);
                    break;
            }
        }

        private void InheritSession()
        {
            m_session = Current.Session;
        }

        private void InitializeNewSession(IsolationLevel isolationLevel)
        {
            m_session = m_factory.OpenSession();
            BeginTransaction(isolationLevel);
        }

        #endregion InitializeScope

        #region PushPop

        /// <summary>
        /// Add <c>scope</c> on top of the scope calling stack. Will be removed during <c>Dispose()</c>.
        /// </summary>
        /// <param name="scope">Scope to be pushed on the calling stack.</param>
        /// <remarks>
        /// Stack structure may contain redundant frames, but this keeps a consistent stack access in
        /// the code.
        /// </remarks>
        private void PushScope(UnitOfWorkScope scope)
        {
            m_savedScope = s_currentScope;
            s_currentScope = scope;
        }

        /// <summary>
        /// Pops the top most stack frame from the scope calling stack and drops its content because
        /// it's no longer needed.
        /// </summary>
        /// <remarks>This won't necessarily exchange the <c>UnitOfWorkScope.Current</c>.</remarks>
        private void PopAndDropScope()
        {
            s_currentScope = m_savedScope;
        }

        #endregion PushPop

        #region SessionHandling

        /// <summary>
        /// Complete Scope.
        /// </summary>
        private void CompleteSession()
        {
            try
            {
                PreCompleteSession();

                if (m_complete && HasActiveTransaction())
                {
                    m_session.Flush();
                    CommitTransaction();
                }
                PostCompleteSession();
            }
            finally
            {
                m_session.Dispose(); // close is not ambient transaction aware, so use dispose instead.
                m_session = null;
            }
        }

        private void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (Transaction.Current == null)
            {
                // no ambient transaction
                m_session.BeginTransaction(isolationLevel);
                m_nhibernateControlsTransaction = true;
            }
            else
            {
                m_nhibernateControlsTransaction = false;
            }
        }

        private void CommitTransaction()
        {
            if (!m_nhibernateControlsTransaction)
            {
                return;
            }
            try
            {
                m_session.Transaction.Commit();
            }
            finally
            {
                m_session.Transaction.Dispose();
            }
        }

        private void RollbackTransaction()
        {
            if (!m_nhibernateControlsTransaction)
            {
                return;
            }
            try
            {
                m_session.Transaction.Rollback();
            }
            finally
            {
                m_session.Transaction.Dispose();
            }
        }

        private bool HasActiveTransaction()
        {
            return m_nhibernateControlsTransaction
                ? m_session.Transaction != null && m_session.Transaction.IsActive
                : (Transaction.Current != null &&
                   Transaction.Current.TransactionInformation.Status == TransactionStatus.Active);
        }

        /// <summary>
        /// Plugin method to execute code before clearing connection cache.
        /// </summary>
        protected virtual void PreCompleteSession()
        {
        }

        /// <summary>
        /// Plugin method to execute code after clearing connection cache.
        /// </summary>
        protected virtual void PostCompleteSession()
        {
        }

        #endregion SessionHandling

        #region Complete

        /// <summary>
        /// Indicates that all operations within the scope are completed successfully.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// This method has already been called once.
        /// </exception>
        public void Complete()
        {
            PreCompleteTransaction();
            m_complete = true;
            PostCompleteTransaction();
        }

        public void InitializeProxy(object proxy)
        {
            if (m_session != null)
            {
                NHibernateUtil.Initialize(proxy);
            }
            else
            {
                throw new InvalidOperationException("No session");
            }
        }

        /// <summary>
        /// Plugin method to execute code before unitofwork scope complete.
        /// </summary>
        protected virtual void PreCompleteTransaction()
        {
        }

        /// <summary>
        /// Plugin method to execute code after unitofwork scope complete.
        /// </summary>
        protected virtual void PostCompleteTransaction()
        {
        }

        #endregion Complete

        #region Dispose

        /// <summary>
        /// Ends the transaction scope. Removes the frame from the scope calling stack... (a) and
        /// close the database connections if this scope was the root scope. (b) Also cleares the
        /// cache if the creation of a new transaction was suppressed or the scope (incl. child
        /// scopes) has an exclusive transaction. In other words: Do not clear the cache if the old
        /// current scope will stay alive.
        /// </summary>
        public void Dispose()
        {
            UnitOfWorkScope oldCurrent = Current;
            if (!m_complete && HasActiveTransaction())
            {
                RollbackTransaction();
            }
            PopAndDropScope();
            try
            {
                if (Current == null)
                {
                    // (a) root scope
                    CompleteSession();
                }
                else if (oldCurrent != Current)
                {
                    // (b) RequiresNew
                    CompleteSession();
                }
            }
            catch (StaleObjectStateException)
            {
                // TODO: add optimistic version here if necessary: throw new OptimisticVersionException();
            }
            finally
            {
                m_session = null;
            }
        }

        #endregion Dispose

        #endregion IMethods
    }
}