namespace YTVisionPro.Modules
{
    internal interface IModule
    {
        int ID { get; set; }

        string Name { get; set; }

        IModuleResult Result { get; set; }

        IModuleParam Param { get; set; }

        double RunTime { get; }

        void Run();
    }
}
