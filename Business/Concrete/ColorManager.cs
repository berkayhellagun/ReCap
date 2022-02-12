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
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color t)
        {
            _colorDal.Add(t);
            return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Delete(Color t)
        {
            _colorDal.Delete(t);
            return new SuccessResult(Messages.ColorRemoved);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == id));
        }

        public IResult Update(Color t)
        {
            _colorDal.Update(t);
            return new SuccessResult(Messages.ColorUpdated);
        }
    }
}
