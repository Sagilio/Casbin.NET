﻿using System.Threading.Tasks;
using NetCasbin.UnitTest.Fixtures;
using Xunit;
using static NetCasbin.UnitTest.Util.TestUtil;

namespace NetCasbin.UnitTest
{
    public class RbacApiTest : IClassFixture<TestModelFixture>
    {
        private readonly TestModelFixture _testModelFixture;

        public RbacApiTest(TestModelFixture testModelFixture)
        {
            _testModelFixture = testModelFixture;
        }

        [Fact]
        public void TestRoleApi()
        {
            var e = new Enforcer(_testModelFixture.GetNewRbacTestModel());
            e.BuildRoleLinks();

            TestGetRoles(e, "alice", AsList("data2_admin"));
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());
            TestGetRoles(e, "non_exist", AsList());

            TestHasRole(e, "alice", "data1_admin", false);
            TestHasRole(e, "alice", "data2_admin", true);

            e.AddRoleForUser("alice", "data1_admin");

            TestGetRoles(e, "alice", AsList("data1_admin", "data2_admin"));
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            e.DeleteRoleForUser("alice", "data1_admin");

            TestGetRoles(e, "alice", AsList("data2_admin"));
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            e.DeleteRolesForUser("alice");

            TestGetRoles(e, "alice", AsList());
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            e.AddRoleForUser("alice", "data1_admin");
            e.DeleteUser("alice");

            TestGetRoles(e, "alice", AsList());
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            e.AddRoleForUser("alice", "data2_admin");

            TestEnforce(e, "alice", "data1", "read", true);
            TestEnforce(e, "alice", "data1", "write", false);

            TestEnforce(e, "alice", "data2", "read", true);
            TestEnforce(e, "alice", "data2", "write", true);

            TestEnforce(e, "bob", "data1", "read", false);
            TestEnforce(e, "bob", "data1", "write", false);
            TestEnforce(e, "bob", "data2", "read", false);
            TestEnforce(e, "bob", "data2", "write", true);

            e.DeleteRole("data2_admin");

            TestEnforce(e, "alice", "data1", "read", true);
            TestEnforce(e, "alice", "data1", "write", false);
            TestEnforce(e, "alice", "data2", "read", false);
            TestEnforce(e, "alice", "data2", "write", false);
            TestEnforce(e, "bob", "data1", "read", false);
            TestEnforce(e, "bob", "data1", "write", false);
            TestEnforce(e, "bob", "data2", "read", false);
            TestEnforce(e, "bob", "data2", "write", true);
        }

        [Fact]
        public async Task TestRoleApiAsync()
        {
            var e = new Enforcer(_testModelFixture.GetNewRbacTestModel());
            e.BuildRoleLinks();

            TestGetRoles(e, "alice", AsList("data2_admin"));
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());
            TestGetRoles(e, "non_exist", AsList());

            TestHasRole(e, "alice", "data1_admin", false);
            TestHasRole(e, "alice", "data2_admin", true);

            await e.AddRoleForUserAsync("alice", "data1_admin");

            TestGetRoles(e, "alice", AsList("data1_admin", "data2_admin"));
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            await e.DeleteRoleForUserAsync("alice", "data1_admin");

            TestGetRoles(e, "alice", AsList("data2_admin"));
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            await e.DeleteRolesForUserAsync("alice");

            TestGetRoles(e, "alice", AsList());
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            await e.AddRoleForUserAsync("alice", "data1_admin");
            await e.DeleteUserAsync("alice");

            TestGetRoles(e, "alice", AsList());
            TestGetRoles(e, "bob", AsList());
            TestGetRoles(e, "data2_admin", AsList());

            await e.AddRoleForUserAsync("alice", "data2_admin");

            TestEnforce(e, "alice", "data1", "read", true);
            TestEnforce(e, "alice", "data1", "write", false);

            TestEnforce(e, "alice", "data2", "read", true);
            TestEnforce(e, "alice", "data2", "write", true);

            TestEnforce(e, "bob", "data1", "read", false);
            TestEnforce(e, "bob", "data1", "write", false);
            TestEnforce(e, "bob", "data2", "read", false);
            TestEnforce(e, "bob", "data2", "write", true);

            await e.DeleteRoleAsync("data2_admin");

            TestEnforce(e, "alice", "data1", "read", true);
            TestEnforce(e, "alice", "data1", "write", false);
            TestEnforce(e, "alice", "data2", "read", false);
            TestEnforce(e, "alice", "data2", "write", false);
            TestEnforce(e, "bob", "data1", "read", false);
            TestEnforce(e, "bob", "data1", "write", false);
            TestEnforce(e, "bob", "data2", "read", false);
            TestEnforce(e, "bob", "data2", "write", true);
        }
    }
}
