//To start server - type - node filename.js from node command prompt.
var http = require('http');
var server = http.createServer();
server.on('request', function(req, res) {
  res.writeHead(200, {
    'content-type': 'text/plain'
  });
  res.write('Hello world! Someone tried to access the server!');
  res.end();
});

var port = 1982;
server.listen(port);
server.once('listening', function() {
  console.log('Hello World server listening on port %d', port);
});
