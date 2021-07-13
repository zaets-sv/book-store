namespace Acme.BookStore.Permissions
{
    public static class BookStorePermissions
    {
        public const string GroupName = "BookStore";

        public static class Books
        {
            public const string Default = GroupName + ".Books";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string MakeOrder = Default + ".MakeOrder";
        }

        public static class Authors
        {
            public const string Default = GroupName + ".Authors";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        } 
        public static class BooksOrder
        {
            public const string Default = GroupName + ".BooksOrder";
            public const string Client = Default + ".Client";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string ChangeStatus = Default + ".ChangeStatus";
            public const string MakeOrder = Default + ".MakeOrder";
            public const string Details = Default + ".Details";
        }
    }
}
