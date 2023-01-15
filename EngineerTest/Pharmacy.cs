namespace EngineerTest;

public class Pharmacy : IPharmacy
{
    private readonly IDrug[] _drugs;

    public Pharmacy(IEnumerable<IDrug> drugs)
    {
        _drugs = drugs.ToArray();
    }

    public IEnumerable<IDrug> UpdateBenefitValue()
    {
        int dailyBenefit = 1;

        for (var i = 0; i < _drugs.Length; i++)
        {
            //Decreases expiration date
            if (_drugs[i].Name != "Magic Pill")
            {
                _drugs[i].ExpiresIn = _drugs[i].ExpiresIn - 1;
            }
            switch (_drugs[i].Name)
            {
                case "Herbal Tea":
                    //Increase by benefit
                    _drugs[i].Benefit += dailyBenefit;
                    //Double if expired
                    if (_drugs[i].ExpiresIn < 0) 
                    {
                        _drugs[i].Benefit += dailyBenefit;
                    }
                    break;
                case "Fervex":
                    //Increase by benefit
                    _drugs[i].Benefit += dailyBenefit;
                    //No need to use floor function as all numbers are integers. Will map 9-5 days to 1 and 4-0 days to 2 (times as fast, extra)
                    if (_drugs[i].ExpiresIn < 10) 
                    {
                        _drugs[i].Benefit += (2 - _drugs[i].ExpiresIn/5) * dailyBenefit;
                    }
                    if (_drugs[i].ExpiresIn < 0) 
                    {
                        _drugs[i].Benefit = 0;
                    }
                    break;
                //Never expires or changes benefit do nothing
                case "Magic Pill":
                    break;
                default:
                    int scale = 1;
                    if (_drugs[i].Name == "Dafalgan") {
                        //Twice as fast
                        scale = 2;
                    } 
                    //Decrease by benefit
                    _drugs[i].Benefit -= scale * dailyBenefit;
                    //Repeat if expired
                    if (_drugs[i].ExpiresIn < 0) 
                    {
                        _drugs[i].Benefit -= scale * dailyBenefit;
                    }
                    break;
            }
            //Checking bounds post change
            if (_drugs[i].Benefit > 50) {
                _drugs[i].Benefit = 50;
            }
            if (_drugs[i].Benefit < 0) {
                _drugs[i].Benefit = 0;
            }
        }

        return _drugs;
    }
}