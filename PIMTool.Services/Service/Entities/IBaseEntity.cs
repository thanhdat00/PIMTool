﻿namespace PIMTool.Services.Service.Entities
{
    public interface IBaseEntity
    {
        int Id
        {
            get; set;
        }
        int RowVersion
        {
            get; set;
        }

    }
}
