(function(){
    'use strict';
    
    // angular
    //     .module('ui.imod', ['ui.bootstrap.datetimepicker'])
    //     .directive('imodDateTimePicker', function(){
    //         return {
    //             restrict:'E',
    //             require: [ 'ngModel'],
    //             scope:{
    //                 inputId: '=',
                    
    //             },
    //             link: function(scope, element, attrs, ctrls){
    //                 // var ngmodel = ctrls[1],
    //                 //     dtpicker = ctrls[0];
    //                 // dtpicker.init(ngmodel);
    //             },
    //             template: ''//<input type="text" ng-model="_will_be_overrriden_" datetime-picker="MM/dd/yyyy HH:mm a" is-open="true"/>'
    //         }
    //     });
    
    
    function controller($scope) {
        $scope.openStartDateCalendar = function(e) { 
            e.preventDefault(); 
            e.stopPropagation(); 
            $scope.startDateIsOpen = true;
        }; 
        $scope.onStartDateBlur = function() {             
                var input = document.getElementById('StartDate').value; 
                var ticks = Date.parse(input); 
                if (ticks) { 
                    $scope.startDateOld = new Date(ticks); 
            }else{
                $scope.startDateOld = null;
            }        
        };
    }

    

    angular
        .module('app', ['ui.bootstrap', 'ui.bootstrap.datetimepicker'])
        .controller('controller', ['$scope', controller])
        .directive('imodDatetimePickerAugumentor', function($parse) {
            var template = '<span class="input-group-btn" style="display: inline-block;"><button type="button" class="btn btn-default"><i class="fa fa-calendar"></i></button></span>';
            function linkFn(scope, element, attrs) {
                var augument = angular.element(template);
                augument.find('button').on('click', function(evt) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    scope.$apply(function() {
                        $parse(attrs.isOpen).assign(scope, true);
                    });
                })
                element.after(augument);
                element.on('blur', function() {                    
                    var ticks = Date.parse(element.val()),
                        scopePropertyToSet = $parse(attrs.ngModel);
                    if (ticks) {
                        scope.$apply(function(){
                            scopePropertyToSet.assign(new Date(ticks));
                        });
                    } else {
                        scope.$apply(function(){
                            scopePropertyToSet.assign(null);
                        });
                    }
                    console.log(scope.startDate);
                })
            }
            return {
                restrict: 'A',
                require: 'datetimePicker',
                link: linkFn
            }
        });    
})();