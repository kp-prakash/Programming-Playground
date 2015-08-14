var SUT = {};

SUT.createTodoItem = function(){
    $("#container").append("<div class='js-todo-item'>A todo item!</div>");
};

SUT.fadeOut = function(duration) {
    $("#container").fadeOut(duration);
};

SUT.fadeOutWithCallBack = function (duration, callback) {
    $("#container").fadeOut(duration, callback);
};

SUT.doSomething = function () {
    //Does nothing other than, throwing an exception.
    throw "Serious error! Contact system administrator.";
};
