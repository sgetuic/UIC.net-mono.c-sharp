using System;

namespace UIC.Util.Extensions {
    public static class ClassExtensions {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
                where TInput : class {
            return o.Return(evaluator, default(TResult));
        }

        public static TResult With<TInput, TResult>(this TInput? o, Func<TInput?, TResult> evaluator)
                where TInput : struct {
            return o.Return(evaluator, default(TResult));
        }


        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
                where TInput : class {
            return (o == null) ? failureValue : evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput? o, Func<TInput?, TResult> evaluator, TResult failureValue)
                where TInput : struct {
            return (o == null) ? failureValue : evaluator(o);
        }

        public static TInput IfNot<TInput>(this TInput o, Func<TInput, bool> evaluator)
                where TInput : class {
            if (o == null)
                return null;
            return !evaluator(o) ? o : null;
        }
    }
}
