using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.DTOs
{
    internal class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly PublishDate { get; set; }
    }
}
