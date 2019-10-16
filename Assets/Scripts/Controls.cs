namespace DefaultNamespace
{
    public class Controls
    {
        private string _up;
        private string _down;
        private string _right;
        private string _left;
        private string _defense;
        private string _kick;
        private string _punch;
        private string _skill;

        public Controls(string up, string down, string right, string left, string defense, string kick, string punch, string skill)
        {
            _up = up;
            _down = down;
            _right = right;
            _left = left;
            _defense = defense;
            _kick = kick;
            _punch = punch;
            _skill = skill;
        }

        public string Up => _up;

        public string Down => _down;

        public string Right => _right;

        public string Left => _left;

        public string Defense => _defense;

        public string Kick => _kick;

        public string Punch => _punch;

        public string Skill => _skill;
    }
}