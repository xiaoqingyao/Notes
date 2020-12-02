using Notes.infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Notes.infrastructure.Validators
{
    public static class Prosecutor
    {

        public static bool NotNull(object obj)
        {
            if (obj == null)
            {
                throw new NotesValidateExceptions("对象为空");
            }
            return true;
        }


        public static bool IsNull<T>(this T root, Expression<Func<T, object>> getter)
        {
            var visitor = new IsNullVisitor
            {
                CurrentObject = root
            };
            visitor.Visit(getter);
            return visitor.IsNull;
        }
    }
}
