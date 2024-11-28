using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Repositories.UserRepository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisTareas.Test.Services
{
    public class TestInsertUserDbContext
    {
        [Fact]
        public async System.Threading.Tasks.Task WhenUserNameDoesNotExist_ThenUserGetsInserted()
        {
            MisTareasContext context = 
                Substitute.For<MisTareasContext>(new DbContextOptions<MisTareasContext>());

            DbSet<User> users = FakeDbSet(new List<User>());
            context.User.Returns(users);

            UserRepository subject = new UserRepository(context);
            var res = subject.Insert(new User { Email = "unittestmail@mail.com"});

            bool result = res is not null ? true : false;

            Assert.True(result);

        }

        private static DbSet<T> FakeDbSet<T>(List<T> data) where T : class
        {
            var _data = data.AsQueryable();
            var fakeDbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            ((IQueryable<T>)fakeDbSet).Provider.Returns(_data.Provider);
            ((IQueryable<T>)fakeDbSet).Expression.Returns(_data.Expression);
            ((IQueryable<T>)fakeDbSet).ElementType.Returns(_data.ElementType);
            ((IQueryable<T>)fakeDbSet).GetEnumerator().Returns(_data.GetEnumerator());

            fakeDbSet.AsQueryable().Returns(fakeDbSet);

            return fakeDbSet;
        }
    }
}
