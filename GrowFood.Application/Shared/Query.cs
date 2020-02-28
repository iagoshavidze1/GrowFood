using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrowFood.Application.Shared
{
    public abstract class Query<T> where T : QueryResponse,new()
    {
        public Task<T> Ok(T res)
        {
            res.Success = true;
            return Task.FromResult(res);
        }

        public Task<T> Fail()
        {
            return Task.FromResult(new T());
        }
        public Task<T> Fail(T res) => Task.FromResult(res);

        public Task<T> Fail(T res, string message)
        {
            res.Message = message;

            return Task.FromResult(res);
        }
    }
}
