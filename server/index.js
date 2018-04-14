var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var port = process.env.PORT || 3000;

http.listen(port, function(){
  console.log('listening on *:' + port);
});

io.sockets.on('connection', function(socket) {

    socket.on('connectToServer', function(nickname){
        socket.username = nickname;
        var string = "Uživatel " + socket.username + " se právě připojil.";
        socket.broadcast.emit('viewNewlyConnected', string);
        console.log("viewNewlyConnected: " + string);
    });

    socket.on('disconnect', function () {
        var string = "Uživatel " + socket.username + " se právě odpojil.";
        socket.broadcast.emit('viewNewlyConnected', string);
    });
});