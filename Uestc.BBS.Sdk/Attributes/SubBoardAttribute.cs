namespace Uestc.BBS.Sdk.Attributes
{
    /// <summary>
    /// 子版块属性
    /// </summary>
    /// <param name="id">typeId</param>
    /// <param name="name">子版块名称</param>
    /// <param name="route">子版块路由地址</param>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public sealed class SubBoardAttribute(int id, string name, string route = "forum/topiclist")
        : Attribute
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; } = id;

        /// <summary>
        /// 名称（如：求助讨论）
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 路由地址（如：forum/topiclist）
        /// </summary>
        public string Route { get; } = route;
    }
}
