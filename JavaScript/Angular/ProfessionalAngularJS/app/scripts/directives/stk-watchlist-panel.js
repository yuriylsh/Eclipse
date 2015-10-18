(function () {
  'use strict';
  
    
  function stkWatchPanel($location, $modal, WatchlistService){
    return {
        templateUrl: 'views/templates/watchlist-panel.html',
        restrict: 'E',
        scope: {},
        link: function (scope) {
          scope.watchlist = {};
          var addListModal = $modal({
            scope: scope,
            template: 'views/templates/addlist-modal.html',
            show: false
          });
          scope.watchlists = WatchlistService.query();
          scope.showModal = function(){
            addListModal.$pomise.then(addListModal.show);
          };
          scope.createList = function(){
            WatchlistService.save(scope.watchlist);
            addListModal.hide();
            scope.watchlist = {};
          };
          scope.deleteList = function (list){
            WatchlistService.remove(list);
            $location.path('/');
          };
        }
      };
  }
  
  angular.module('stockDogApp')
    .directive('stkWatchlistPanel', ['$location', '$modal', 'WatchlistService',stkWatchPanel]);

} ());