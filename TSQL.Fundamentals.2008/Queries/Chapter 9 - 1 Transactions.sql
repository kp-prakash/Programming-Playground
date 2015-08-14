/*Transactions*/
/*A transaction is a unit of work that may include multiple 
activities that query and modify data and that possibly 
change data definition.

BEGIN TRAN - Being a transaction.
COMMIT TRAN - Commit a transaction.
ROLLBACK TRAN - Rollback a transaction.
*/

/*If you do not mark the boundaries of a transaction explicitly,
by default SQL Server treats each individual statement as a 
transaction; in other words, by default SQL Server automatically 
commits the transaction at the end of each individual statement. 
You can change the way SQL Server handles implicit transactions 
with a session option called IMPLICIT_TRANSACTIONS. This option 
is off by default. When this option is on, you do not have to 
specify the BEGIN TRAN statement to mark the beginning of a 
transaction, but you have to mark the transaction's end with 
a COMMIT TRAN or a ROLLBACK TRAN statement.

ACID - Atomicity Consistency Isolation Durability

At any point in your code you can tell programmatically whether 
you are in an open transaction by querying a function called 
@@TRANCOUNT. This function returns 0 if you're not in an open 
transaction, and a value greater than 0 if you are.


 */
 