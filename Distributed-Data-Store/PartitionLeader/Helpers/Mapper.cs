﻿using PartitionLeader.Models;

namespace PartitionLeader.Helpers;

public static class Mapper
{
    public static Data Map(this DataModel dataModel)
    {
        return new Data()
        {
            File = (FormFile) dataModel.File,
            Id = dataModel.Id
        };
    }
}