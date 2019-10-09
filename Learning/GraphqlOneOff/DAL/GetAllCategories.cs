using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphqlOneOff.GraphQl.Types;
using LanguageExt;

namespace GraphqlOneOff.DAL
{
    public delegate IEnumerable<Category> GetAllCategories();

    public delegate IEnumerable<Category> GetDescendants(int id);
    public delegate Task<ILookup<int, Category>> GetDescendantsBatched(IEnumerable<int> parents, CancellationToken token);

    public static class FakeDataProvider
    {
        private static readonly Person[] People = 
        {
            new Person
            {
                Id = 1, FirstName = "Egbert", LastName = "Wassung", Email = "ewassung0@ebay.com", Gender = "Male",
                IpAddress = "19.225.186.159", IsChild = true
            },
            new Person
            {
                Id = 2, FirstName = "Ephraim", LastName = "Haresnaip", Email = "eharesnaip1@rakuten.co.jp",
                Gender = "Male", IpAddress = "114.16.172.243", IsChild = false
            },
            new Person
            {
                Id = 3, FirstName = "Lorne", LastName = "McGeechan", Email = "lmcgeechan2@chronoengine.com",
                Gender = "Female", IpAddress = "2.69.245.191", IsChild = false
            },
            new Person
            {
                Id = 4, FirstName = "Toby", LastName = "Andreotti", Email = "tandreotti3@mtv.com", Gender = "Male",
                IpAddress = "56.5.92.99", IsChild = false
            },
            new Person
            {
                Id = 5, FirstName = "Joannes", LastName = "Memmory", Email = "jmemmory4@bloglovin.com",
                Gender = "Female", IpAddress = "129.59.232.88", IsChild = true
            },
            new Person
            {
                Id = 6, FirstName = "Markus", LastName = "Pinard", Email = "mpinard5@craigslist.org", Gender = "Male",
                IpAddress = "43.111.73.245", IsChild = false
            },
            new Person
            {
                Id = 7, FirstName = "Pammi", LastName = "Gatling", Email = "pgatling6@wunderground.com",
                Gender = "Female", IpAddress = "192.115.245.197", IsChild = true
            },
            new Person
            {
                Id = 8, FirstName = "Chas", LastName = "Gooms", Email = "cgooms7@wikispaces.com", Gender = "Male",
                IpAddress = "183.116.34.86", IsChild = true
            },
            new Person
            {
                Id = 9, FirstName = "Delcine", LastName = "Davidovits", Email = "ddavidovits8@jimdo.com",
                Gender = "Female", IpAddress = "147.19.62.188", IsChild = true
            },
            new Person
            {
                Id = 10, FirstName = "Nathaniel", LastName = "Storr", Email = "nstorr9@digg.com", Gender = "Male",
                IpAddress = "218.190.103.205", IsChild = false
            },
            new Person
            {
                Id = 11, FirstName = "Marcus", LastName = "Geater", Email = "mgeatera@pbs.org", Gender = "Male",
                IpAddress = "26.14.86.164", IsChild = false
            },
            new Person
            {
                Id = 12, FirstName = "Hardy", LastName = "Agostini", Email = "hagostinib@adobe.com", Gender = "Male",
                IpAddress = "177.220.204.234", IsChild = true
            },
            new Person
            {
                Id = 13, FirstName = "Lucky", LastName = "Ossipenko", Email = "lossipenkoc@home.pl", Gender = "Female",
                IpAddress = "64.223.77.157", IsChild = false
            },
            new Person
            {
                Id = 14, FirstName = "Alfi", LastName = "Ebrall", Email = "aebralld@example.com", Gender = "Female",
                IpAddress = "3.182.56.94", IsChild = true
            },
            new Person
            {
                Id = 15, FirstName = "Hilary", LastName = "Szantho", Email = "hszanthoe@ox.ac.uk", Gender = "Male",
                IpAddress = "120.73.75.120", IsChild = true
            },
            new Person
            {
                Id = 16, FirstName = "Sherye", LastName = "Gaddes", Email = "sgaddesf@nytimes.com", Gender = "Female",
                IpAddress = "182.108.92.48", IsChild = false
            },
            new Person
            {
                Id = 17, FirstName = "Garreth", LastName = "Hinz", Email = "ghinzg@icio.us", Gender = "Male",
                IpAddress = "52.97.85.148", IsChild = true
            },
            new Person
            {
                Id = 18, FirstName = "Brenna", LastName = "Ullrich", Email = "bullrichh@infoseek.co.jp",
                Gender = "Female", IpAddress = "6.20.49.2", IsChild = true
            },
            new Person
            {
                Id = 19, FirstName = "Willem", LastName = "Grinishin", Email = "wgrinishini@sohu.com", Gender = "Male",
                IpAddress = "131.3.189.66", IsChild = false
            },
            new Person
            {
                Id = 20, FirstName = "Sampson", LastName = "Massot", Email = "smassotj@canalblog.com", Gender = "Male",
                IpAddress = "88.139.6.175", IsChild = false
            },
            new Person
            {
                Id = 21, FirstName = "Gianna", LastName = "Boulstridge", Email = "gboulstridgek@livejournal.com",
                Gender = "Female", IpAddress = "247.157.13.105", IsChild = true
            },
            new Person
            {
                Id = 22, FirstName = "Raimundo", LastName = "Fraczak", Email = "rfraczakl@bizjournals.com",
                Gender = "Male", IpAddress = "38.240.250.98", IsChild = true
            },
            new Person
            {
                Id = 23, FirstName = "Janine", LastName = "Doxey", Email = "jdoxeym@wisc.edu", Gender = "Female",
                IpAddress = "19.87.70.250", IsChild = false
            },
            new Person
            {
                Id = 24, FirstName = "Eba", LastName = "Edmeades", Email = "eedmeadesn@ehow.com", Gender = "Female",
                IpAddress = "44.165.169.130", IsChild = true
            },
            new Person
            {
                Id = 25, FirstName = "Vladamir", LastName = "Willeson", Email = "vwillesono@moonfruit.com",
                Gender = "Male", IpAddress = "199.119.84.48", IsChild = false
            },
            new Person
            {
                Id = 26, FirstName = "Olympe", LastName = "Woollhead", Email = "owoollheadp@chron.com",
                Gender = "Female", IpAddress = "198.240.65.212", IsChild = false
            },
            new Person
            {
                Id = 27, FirstName = "Teodora", LastName = "Bignall", Email = "tbignallq@nifty.com", Gender = "Female",
                IpAddress = "47.226.74.56", IsChild = true
            },
            new Person
            {
                Id = 28, FirstName = "Cornelius", LastName = "Munro", Email = "cmunror@wsj.com", Gender = "Male",
                IpAddress = "68.148.66.17", IsChild = true
            },
            new Person
            {
                Id = 29, FirstName = "Claudio", LastName = "Breukelman", Email = "cbreukelmans@1und1.de",
                Gender = "Male", IpAddress = "223.15.63.230", IsChild = false
            },
            new Person
            {
                Id = 30, FirstName = "Gearard", LastName = "Krikorian", Email = "gkrikoriant@virginia.edu",
                Gender = "Male", IpAddress = "113.142.108.58", IsChild = true
            },
            new Person
            {
                Id = 31, FirstName = "Jerrold", LastName = "Winsborrow", Email = "jwinsborrowu@fotki.com",
                Gender = "Male", IpAddress = "77.0.187.193", IsChild = false
            },
            new Person
            {
                Id = 32, FirstName = "Eric", LastName = "Piner", Email = "epinerv@angelfire.com", Gender = "Male",
                IpAddress = "198.255.248.154", IsChild = true
            },
            new Person
            {
                Id = 33, FirstName = "Antonietta", LastName = "Yggo", Email = "ayggow@cdbaby.com", Gender = "Female",
                IpAddress = "139.54.64.250", IsChild = false
            },
            new Person
            {
                Id = 34, FirstName = "Krissy", LastName = "Favell", Email = "kfavellx@slashdot.org", Gender = "Female",
                IpAddress = "204.150.47.233", IsChild = false
            },
            new Person
            {
                Id = 35, FirstName = "Beverly", LastName = "O", Email = "Kinedy,bokinedyy@nps.gov", Gender = "Female",
                IpAddress = "60.109.158.237", IsChild = false
            },
            new Person
            {
                Id = 36, FirstName = "Karlotta", LastName = "Leidl", Email = "kleidlz@howstuffworks.com",
                Gender = "Female", IpAddress = "160.244.192.6", IsChild = false
            },
            new Person
            {
                Id = 37, FirstName = "Brooks", LastName = "Essame", Email = "bessame10@instagram.com", Gender = "Male",
                IpAddress = "117.32.34.42", IsChild = false
            },
            new Person
            {
                Id = 38, FirstName = "Hagan", LastName = "Whitwham", Email = "hwhitwham11@berkeley.edu",
                Gender = "Male", IpAddress = "170.118.64.138", IsChild = true
            },
            new Person
            {
                Id = 39, FirstName = "Sutton", LastName = "McSwan", Email = "smcswan12@4shared.com", Gender = "Male",
                IpAddress = "31.175.29.179", IsChild = false
            },
            new Person
            {
                Id = 40, FirstName = "Patty", LastName = "Turnell", Email = "pturnell13@digg.com", Gender = "Male",
                IpAddress = "230.50.188.75", IsChild = true
            },
            new Person
            {
                Id = 41, FirstName = "Raina", LastName = "Tarbin", Email = "rtarbin14@newyorker.com", Gender = "Female",
                IpAddress = "231.8.43.192", IsChild = true
            },
            new Person
            {
                Id = 42, FirstName = "Eve", LastName = "Hooban", Email = "ehooban15@redcross.org", Gender = "Female",
                IpAddress = "200.47.25.189", IsChild = false
            },
            new Person
            {
                Id = 43, FirstName = "Koenraad", LastName = "Aglione", Email = "kaglione16@yahoo.co.jp",
                Gender = "Male", IpAddress = "251.171.231.102", IsChild = false
            },
            new Person
            {
                Id = 44, FirstName = "Sancho", LastName = "Kayley", Email = "skayley17@wired.com", Gender = "Male",
                IpAddress = "50.126.0.89", IsChild = false
            },
            new Person
            {
                Id = 45, FirstName = "Che", LastName = "Evequot", Email = "cevequot18@narod.ru", Gender = "Male",
                IpAddress = "95.75.223.145", IsChild = false
            },
            new Person
            {
                Id = 46, FirstName = "Hewett", LastName = "Sedgeworth", Email = "hsedgeworth19@g.co", Gender = "Male",
                IpAddress = "103.119.175.4", IsChild = false
            },
            new Person
            {
                Id = 47, FirstName = "Stafani", LastName = "Flancinbaum", Email = "sflancinbaum1a@reference.com",
                Gender = "Female", IpAddress = "149.214.110.114", IsChild = false
            },
            new Person
            {
                Id = 48, FirstName = "Gerick", LastName = "Admans", Email = "gadmans1b@netvibes.com", Gender = "Male",
                IpAddress = "141.236.105.146", IsChild = true
            },
            new Person
            {
                Id = 49, FirstName = "Gina", LastName = "Bennell", Email = "gbennell1c@e-recht24.de", Gender = "Female",
                IpAddress = "164.95.222.99", IsChild = true
            },
            new Person
            {
                Id = 50, FirstName = "Jessie", LastName = "Burnup", Email = "jburnup1d@microsoft.com", Gender = "Male",
                IpAddress = "165.124.58.123", IsChild = true
            },
            new Person
            {
                Id = 51, FirstName = "Karlotta", LastName = "Beloe", Email = "kbeloe1e@mayoclinic.com",
                Gender = "Female", IpAddress = "216.110.254.52", IsChild = false
            },
            new Person
            {
                Id = 52, FirstName = "Kordula", LastName = "Gookey", Email = "kgookey1f@deviantart.com",
                Gender = "Female", IpAddress = "104.24.172.232", IsChild = true
            },
            new Person
            {
                Id = 53, FirstName = "Darlene", LastName = "Ablewhite", Email = "dablewhite1g@admin.ch",
                Gender = "Female", IpAddress = "172.51.229.196", IsChild = true
            },
            new Person
            {
                Id = 54, FirstName = "Karry", LastName = "Wanden", Email = "kwanden1h@eventbrite.com",
                Gender = "Female", IpAddress = "140.224.56.67", IsChild = true
            },
            new Person
            {
                Id = 55, FirstName = "Cornelius", LastName = "Lodo", Email = "clodo1i@irs.gov", Gender = "Male",
                IpAddress = "15.13.122.158", IsChild = false
            },
            new Person
            {
                Id = 56, FirstName = "Maritsa", LastName = "Hurll", Email = "mhurll1j@51.la", Gender = "Female",
                IpAddress = "120.15.17.73", IsChild = true
            },
            new Person
            {
                Id = 57, FirstName = "Marianna", LastName = "Ede", Email = "mede1k@jimdo.com", Gender = "Female",
                IpAddress = "15.83.156.241", IsChild = false
            },
            new Person
            {
                Id = 58, FirstName = "Archambault", LastName = "Giacovetti", Email = "agiacovetti1l@sohu.com",
                Gender = "Male", IpAddress = "31.232.23.153", IsChild = false
            },
            new Person
            {
                Id = 59, FirstName = "Paula", LastName = "Bigly", Email = "pbigly1m@hao123.com", Gender = "Female",
                IpAddress = "52.56.123.88", IsChild = true
            },
            new Person
            {
                Id = 60, FirstName = "Alie", LastName = "Feldmesser", Email = "afeldmesser1n@mozilla.org",
                Gender = "Female", IpAddress = "52.10.74.70", IsChild = false
            },
            new Person
            {
                Id = 61, FirstName = "Selle", LastName = "Havoc", Email = "shavoc1o@istockphoto.com", Gender = "Female",
                IpAddress = "91.37.135.195", IsChild = true
            },
            new Person
            {
                Id = 62, FirstName = "Kristian", LastName = "Sichardt", Email = "ksichardt1p@addthis.com",
                Gender = "Male", IpAddress = "156.37.237.48", IsChild = true
            },
            new Person
            {
                Id = 63, FirstName = "Maurizio", LastName = "Bacchus", Email = "mbacchus1q@reference.com",
                Gender = "Male", IpAddress = "232.27.157.124", IsChild = false
            },
            new Person
            {
                Id = 64, FirstName = "Tabina", LastName = "Highnam", Email = "thighnam1r@google.de", Gender = "Female",
                IpAddress = "35.153.154.63", IsChild = false
            },
            new Person
            {
                Id = 65, FirstName = "Bianka", LastName = "Kloska", Email = "bkloska1s@networkadvertising.org",
                Gender = "Female", IpAddress = "166.56.100.53", IsChild = true
            },
            new Person
            {
                Id = 66, FirstName = "Gypsy", LastName = "Tams", Email = "gtams1t@blogs.com", Gender = "Female",
                IpAddress = "136.94.138.13", IsChild = true
            },
            new Person
            {
                Id = 67, FirstName = "Stephan", LastName = "Peterkin", Email = "speterkin1u@house.gov", Gender = "Male",
                IpAddress = "150.184.209.108", IsChild = true
            },
            new Person
            {
                Id = 68, FirstName = "Henrietta", LastName = "Camings", Email = "hcamings1v@t.co", Gender = "Female",
                IpAddress = "97.42.114.105", IsChild = true
            },
            new Person
            {
                Id = 69, FirstName = "Grace", LastName = "Tomasutti", Email = "gtomasutti1w@so-net.ne.jp",
                Gender = "Male", IpAddress = "210.38.198.66", IsChild = true
            },
            new Person
            {
                Id = 70, FirstName = "Angelika", LastName = "Baudacci", Email = "abaudacci1x@tripadvisor.com",
                Gender = "Female", IpAddress = "175.142.228.183", IsChild = true
            },
            new Person
            {
                Id = 71, FirstName = "Ken", LastName = "Gonnelly", Email = "kgonnelly1y@mac.com", Gender = "Male",
                IpAddress = "11.204.16.112", IsChild = true
            },
            new Person
            {
                Id = 72, FirstName = "Darla", LastName = "Abramsky", Email = "dabramsky1z@homestead.com",
                Gender = "Female", IpAddress = "216.188.32.47", IsChild = false
            },
            new Person
            {
                Id = 73, FirstName = "Imogene", LastName = "Gurrado", Email = "igurrado20@dell.com", Gender = "Female",
                IpAddress = "114.26.98.64", IsChild = false
            },
            new Person
            {
                Id = 74, FirstName = "Andrea", LastName = "Godmer", Email = "agodmer21@odnoklassniki.ru",
                Gender = "Male", IpAddress = "212.236.51.242", IsChild = false
            },
            new Person
            {
                Id = 75, FirstName = "Anastasia", LastName = "Screeton", Email = "ascreeton22@ustream.tv",
                Gender = "Female", IpAddress = "186.19.201.89", IsChild = true
            },
            new Person
            {
                Id = 76, FirstName = "Candra", LastName = "Capper", Email = "ccapper23@meetup.com", Gender = "Female",
                IpAddress = "196.123.119.37", IsChild = true
            },
            new Person
            {
                Id = 77, FirstName = "Hunt", LastName = "Gearty", Email = "hgearty24@bloomberg.com", Gender = "Male",
                IpAddress = "216.183.201.102", IsChild = true
            },
            new Person
            {
                Id = 78, FirstName = "Kelsi", LastName = "Eamer", Email = "keamer25@tiny.cc", Gender = "Female",
                IpAddress = "134.28.188.26", IsChild = true
            },
            new Person
            {
                Id = 79, FirstName = "Clareta", LastName = "Collimore", Email = "ccollimore26@kickstarter.com",
                Gender = "Female", IpAddress = "137.22.64.22", IsChild = false
            },
            new Person
            {
                Id = 80, FirstName = "Roxanne", LastName = "Sextie", Email = "rsextie27@tuttocitta.it",
                Gender = "Female", IpAddress = "142.79.76.253", IsChild = true
            },
            new Person
            {
                Id = 81, FirstName = "Wadsworth", LastName = "Longina", Email = "wlongina28@google.pl", Gender = "Male",
                IpAddress = "79.84.155.228", IsChild = true
            },
            new Person
            {
                Id = 82, FirstName = "Hamid", LastName = "Hanhart", Email = "hhanhart29@vkontakte.ru", Gender = "Male",
                IpAddress = "37.66.136.230", IsChild = false
            },
            new Person
            {
                Id = 83, FirstName = "Rhoda", LastName = "Pierrepont", Email = "rpierrepont2a@comcast.net",
                Gender = "Female", IpAddress = "99.15.10.25", IsChild = true
            },
            new Person
            {
                Id = 84, FirstName = "Stevie", LastName = "Pillinger", Email = "spillinger2b@uiuc.edu", Gender = "Male",
                IpAddress = "112.121.15.25", IsChild = false
            },
            new Person
            {
                Id = 85, FirstName = "Marisa", LastName = "McGorman", Email = "mmcgorman2c@reddit.com",
                Gender = "Female", IpAddress = "112.168.208.231", IsChild = false
            },
            new Person
            {
                Id = 86, FirstName = "Kameko", LastName = "Vasyutkin", Email = "kvasyutkin2d@github.com",
                Gender = "Female", IpAddress = "116.125.153.81", IsChild = false
            },
            new Person
            {
                Id = 87, FirstName = "Tobin", LastName = "Ruddin", Email = "truddin2e@si.edu", Gender = "Male",
                IpAddress = "250.97.246.223", IsChild = true
            },
            new Person
            {
                Id = 88, FirstName = "Mignonne", LastName = "Maddicks", Email = "mmaddicks2f@addtoany.com",
                Gender = "Female", IpAddress = "81.224.73.2", IsChild = false
            },
            new Person
            {
                Id = 89, FirstName = "Francisca", LastName = "Bogeys", Email = "fbogeys2g@themeforest.net",
                Gender = "Female", IpAddress = "167.163.168.21", IsChild = false
            },
            new Person
            {
                Id = 90, FirstName = "Moira", LastName = "Ronald", Email = "mronald2h@elegantthemes.com",
                Gender = "Female", IpAddress = "163.7.249.21", IsChild = false
            },
            new Person
            {
                Id = 91, FirstName = "Cody", LastName = "Bastow", Email = "cbastow2i@cargocollective.com",
                Gender = "Female", IpAddress = "226.154.6.76", IsChild = true
            },
            new Person
            {
                Id = 92, FirstName = "Germaine", LastName = "Whanstall", Email = "gwhanstall2j@nifty.com",
                Gender = "Male", IpAddress = "39.81.134.2", IsChild = false
            },
            new Person
            {
                Id = 93, FirstName = "Jessalin", LastName = "Shilling", Email = "jshilling2k@google.de",
                Gender = "Female", IpAddress = "129.154.10.200", IsChild = false
            },
            new Person
            {
                Id = 94, FirstName = "Perry", LastName = "Aspinwall", Email = "paspinwall2l@de.vu", Gender = "Male",
                IpAddress = "159.207.21.169", IsChild = true
            },
            new Person
            {
                Id = 95, FirstName = "Ronda", LastName = "Chastaing", Email = "rchastaing2m@wikipedia.org",
                Gender = "Female", IpAddress = "16.246.89.176", IsChild = true
            },
            new Person
            {
                Id = 96, FirstName = "Auroora", LastName = "Fendt", Email = "afendt2n@constantcontact.com",
                Gender = "Female", IpAddress = "238.157.137.68", IsChild = false
            },
            new Person
            {
                Id = 97, FirstName = "Skip", LastName = "Andrassy", Email = "sandrassy2o@hatena.ne.jp", Gender = "Male",
                IpAddress = "41.104.65.134", IsChild = false
            },
            new Person
            {
                Id = 98, FirstName = "Shirline", LastName = "Eicke", Email = "seicke2p@shareasale.com",
                Gender = "Female", IpAddress = "89.4.125.187", IsChild = false
            },
            new Person
            {
                Id = 99, FirstName = "Loleta", LastName = "Bellie", Email = "lbellie2q@typepad.com", Gender = "Female",
                IpAddress = "215.32.212.181", IsChild = false
            },
            new Person
            {
                Id = 100, FirstName = "Reggi", LastName = "Brogini", Email = "rbrogini2r@storify.com",
                Gender = "Female", IpAddress = "203.52.99.162", IsChild = true
            },
        };

        public static IEnumerable<Category> GetAllCategories() =>
            People.Where(x => !x.IsChild).Select(PersonToCategory).ToArray();
        public static Task<ILookup<int, Category>> GetDescendantsBatchedImplementation(IEnumerable<int> parents,CancellationToken token)
        {
            var getChildren = GetDescendantCategories();
            var lookup = parents.SelectMany(x => getChildren(x).Select(child => (x, child))).ToLookup(x => x.x, x => x.child);
            return Task.FromResult(lookup);
        }

        public static GetDescendants GetDescendantCategories() => new GetDescendants(Prelude.par<Category[], int, IEnumerable<Category>>(GetChildren, Children));

        private static Category[] Children => People.Where(x => x.IsChild).Select(PersonToCategory).ToArray();

        private static Category PersonToCategory(Person p) => new Category {Id = p.Id, Name = $"{p.FirstName} {p.LastName}"};

        private static IEnumerable<Category> GetChildren(Category[] children, int id)
        {
            Debug.WriteLine($"Getting descendants for {id}");
            return Enumerable.Range(0, Rnd.Next(0, 3)).Map(_ => children[Rnd.Next(0, children.Length - 1)]);
        }

        

        private static readonly Random Rnd = new Random((int)DateTime.UtcNow.Ticks);

        class Person
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string IpAddress { get; set; }
            public bool IsChild { get; set; }
        }

        
    }
}