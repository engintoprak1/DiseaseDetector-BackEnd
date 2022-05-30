using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<QuestionManager>().As<IQuestionService>().SingleInstance();
            builder.RegisterType<PoliclinicManager>().As<IPoliclinicService>().SingleInstance();
            builder.RegisterType<HospitalManager>().As<IHospitalService>().SingleInstance();
            builder.RegisterType<AnswerManager>().As<IAnswerService>().SingleInstance();

            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>().SingleInstance();
            builder.RegisterType<EfPoliclinicDal>().As<IPoliclinicDal>().SingleInstance();
            builder.RegisterType<EfHospitalDal>().As<IHospitalDal>().SingleInstance();
            builder.RegisterType<EfAnswerDal>().As<IAnswerDal>().SingleInstance();
            builder.RegisterType<EfUserQuestionAnswerDal>().As<IUserQuestionAnswerDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}

