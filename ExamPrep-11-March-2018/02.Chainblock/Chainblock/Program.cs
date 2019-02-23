using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        IChainblock cb = new Chainblock();
        Transaction tx1 = new Transaction(5, TransactionStatus.Successfull, "joro", "pesho", 1);
        Transaction tx2 = new Transaction(6, TransactionStatus.Aborted, "joro", "pesho", 5.5);
        Transaction tx3 = new Transaction(7, TransactionStatus.Aborted, "joro", "pesho", 5.5);
        Transaction tx4 = new Transaction(12, TransactionStatus.Failed, "joro", "pesho", 15.6);
        Transaction tx5 = new Transaction(15, TransactionStatus.Successfull, "joro", "pesho", 7.8);
        //Act
        cb.Add(tx1);
        cb.Add(tx3);
        cb.Add(tx2);
        cb.Add(tx4);
        cb.Add(tx5);
        //Assert
        var res = cb.GetAllSendersWithTransactionStatus(TransactionStatus.Unauthorized);
    }
}
