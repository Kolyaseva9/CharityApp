namespace InstructorRating
{
    public class Donator
    {
        public const int DefaultSum = 1000;

        private string _name;

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        private int _sum;

        public int Sum {
            get { return _sum; }
            set { _sum = value; }
        }

        public string Info {
            get {
                return $"{_name} - {_sum} - {_charity.Name} - {_charity.Address}";
            }
        }

        private Charity _charity;
        public Charity Charity {
            get { return _charity; }
            set { _charity = value; }
        }

        public Donator(string name, int sum) {
            _name = name;
            _sum = sum;
        }

        public Donator(string name) 
            : this(name, DefaultSum) {
        }
    }
}
