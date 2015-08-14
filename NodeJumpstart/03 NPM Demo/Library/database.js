var async = require('async');
console.log(async.toString()); 
//The output in node console is '[object Object]'
//This indicates that node.js traverses the file tree and loads the modules.