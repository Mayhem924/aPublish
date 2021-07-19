using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPublish.Model;

namespace aPublish.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        private List<Post> _posts = new List<Post>();

        public IEnumerable<Post> Posts => _posts;
    }
}
