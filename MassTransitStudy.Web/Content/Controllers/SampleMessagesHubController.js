angular.module('SampleMessagesHub', [])
    .controller(
        'SampleMessagesHubController',
        function ($scope) {
            $scope.message = '';
            $scope.messages = [];
            $scope.startIndex = 0;
            $scope.numberOfItems = 100;

            $scope.messagesHub = $.connection.sampleMessagesHub;
            $.connection.hub.start();

            $scope.messagesHub.client.addListOfMessagesToPage = function(message) {
                $scope.messages = message.Result;
                $scope.startIndex = message.StartIndex;
                $scope.numberOfMessages = message.NumberOfItems;
            };

            $scope.messagesHub.client.addNewMessageToPage = function(message) {
                $scope.messages.push(message);
            };

            $scope.loadMessages = function() {
                $scope.messagesHub.server.getSampleMessagesList($scope.startIndex, $scope.numberOfItems);
            };

            $scope.sendMessage = function() {
                $scope.messagesHub.server.send($scope.message);
            };

            $scope.clearMessages = function() {
                $scope.messages = [];
            }
        })
    .directive(
        'sampleMessagesTable',
        function() {
            return {
                restrict: 'E',
                templateUrl: 'Content/Directives/sample-messages-table.html'
            };
        });