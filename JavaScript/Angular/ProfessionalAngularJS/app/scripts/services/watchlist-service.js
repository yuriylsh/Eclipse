(function () {
	'use strict';
	angular
		.module('stockDogApp')
		.service('WatchlistService', WatchListService);
	var Model = loadModel();
	function WatchListService(){
		this.query = function(listId){
			if(listId){
				return findById(listId);
			}else{
				return Model.watchlists;
			}
		};
		this.save = function(watchList){
			watchList.id = Model.nextId++;
			Model.watchlists.push(watchList);
			saveModel();	
		};
		this.remove = function(watchList){
			_.remove(Model.watchlists, function(list){
				return list.id == watchList.id;
			});
		};
	}
	
	function loadModel(){
		var watchlists = localStorage['StockDog.watchlists'];
		var nextId = localStorage['StockDog.nextId'];
		var model = {
			watchlists: watchlists
				? JSON.parse(watchlists)
				: [],
			nextId: nextId
				? parseInt(nextId)
				: 0
		};
		return model;
	}
	function saveModel(){
		localStorage['StockDog.watchlists'] = JSON.stringify(Model.watchlists);
		localStorage['StockDog.nextId'] = Model.nextId;		
	}
	function findById(listId){
		return _.find(Model.watchlists, function(watchlist){
			return watchlist.id === parseInt(listId);
		});
	}
} ());
