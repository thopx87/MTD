using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTD.Helper
{
    public enum EnumError
    {
        INPUT  = -1,
        EXISTS = -2,
        NOT_EXISTS = -3,
        UNKNOWN = -4,
        BLOCK = -5,
        NOT_LOGIN = -6,
        ROLE_WRONG = -7,
        INSERT_ERROR = -8,
        UPDATE_ERROR = -9,

        NOT_FOUND = -404
    }
}