using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MTD.DAL;

namespace MTD.Services
{
    public abstract class BaseService: IDisposable
    {
        protected MTDDataContext _context;

        public BaseService()
        {
            if (_context == null)
            {
                _context = new MTDDataContext();
            }
        }
        public void Dispose()
        {
            _context.SubmitChanges();
            _context.Dispose();
        }

        public MTDDataContext Context { get { return _context; } }
    }
}