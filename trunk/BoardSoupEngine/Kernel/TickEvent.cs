namespace BoardSoupEngine.Kernel
{
    internal class TickEvent : KernelEvent
    {
        public TickEvent()
        {
        }

        public override void execute(IEventListener module)
        {
            module.onTick();
        }
    }
}
