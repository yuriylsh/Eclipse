(function () {
	'use strict';
	angular
		.module('store', [])
		.controller('StoreController', ['testService',StoreController])
        .provider('testService', function() {
	        this.$get = function() {
	            return { serviceValue: 'Yuriy' };
	        };
	    });
	window.app.value('peopleData', {test: 'test'});
	function StoreController(testService) {
	    this.products = gems;
	    this.test = testService.serviceValue;
	    console.log(window);
	};

	var gems = [
		{
			name: 'Dodecahedron',
			price: 2.95,
			description: '...',
			canPurchase: true,
			soldOut: true
		},
		{
			name: 'Pentagonal Gem',
			price: 5.99,
			description: '...',
			canPurchase: false
		}
	];
} ());