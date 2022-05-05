using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public async Task<IResult> AsyncAdd(Color t)
        {
            await _colorDal.AsyncAdd(t);
            return new SuccessResult(Messages.ColorAdded);
        }

        public async Task<IResult> AsyncDelete(Color t)
        {
            await _colorDal.AsyncDelete(t);
            return new SuccessResult(Messages.ColorRemoved);
        }

        public async Task<IDataResult<List<Color>>> AsyncGetAll()
        {
            return new SuccessDataResult<List<Color>>(await _colorDal.AsyncGetAll());
        }

        public async Task<IDataResult<Color>> AsyncGetById(int id)
        {
            return new SuccessDataResult<Color>(await _colorDal.AsyncGet(c => c.Id == id));
        }

        public async Task<IResult> AsyncUpdate(Color t)
        {
            await _colorDal.AsyncUpdate(t);
            return new SuccessResult(Messages.ColorUpdated);
        }
    }
}
