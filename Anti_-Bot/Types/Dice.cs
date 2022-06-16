namespace Anti__Bot
{
    public class DiceInfo
    {
        public int Number { get; set; } = 1;
        public int Dice { get; set; } = 10;

        /// <summary>
        /// d or к
        /// </summary>
        public string Name { get; set; } = "1d10";

        public int Target { get; set; }

        // Pool props------------------------------
        public bool Pool { get; set; }

        public int PoolTarget { get; set; } = 6;
        public int PoolSkillLevel { get; set; }

        public int FailIgoreLevel { get; set; }

        // Result----------------------------------
        public string Result { get; set; }
    }
}