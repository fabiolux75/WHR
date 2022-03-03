using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class DataTableModel
    {
        int isIncluded { get; set; }
        int draw { get; set; }
        int start { get; set; }
        int length { get; set; }

    }
}



/*
 * 
 isIncluded: 0
draw: 1
columns[0][data]: lastName
columns[0][name]: 
columns[0][searchable]: true
columns[0][orderable]: true
columns[0][search][value]: 
columns[0][search][regex]: false
columns[1][data]: firstName
columns[1][name]: 
columns[1][searchable]: true
columns[1][orderable]: true
columns[1][search][value]: 
columns[1][search][regex]: false
order[0][column]: 0
order[0][dir]: asc
start: 0
length: 10
search[value]: 
search[regex]: false
 */
