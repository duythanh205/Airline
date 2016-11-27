"use strict";
var app = angular.module("ailineApp", [
    'ngResource',
    'ngRoute',
    'ngAnimate',
    'ngSanitize',

    'ui.bootstrap.datetimepicker',
    'angularModalService'

]);

app.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});

app.directive('dateTimePicker', ['$timeout', function ($timeout) {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, element, attrs, ngModelCtrl) {
            var parent = $(element).parent();
            var dtp = parent.datetimepicker({
                format: "LL",
                showTodayButton: true,
            });
            dtp.on("dp.change", function (e) {
                ngModelCtrl.$setViewValue(moment(e.date).format("LL"));
                scope.$apply();
            });
        }
    };
}]);

app.service('ApiCall', ['$http', function ($http) {
    var result;

    // This is used for calling get methods from web api
    this.GetApiCall = function (url) {
        result = $http.get(url).success(function (data, status) {
            result = (data);
        }).error(function () {
            alert("Something went wrong");
        });
        return result;
    };

    // This is used for calling post methods from web api with passing some data to the web api controller
    this.PostApiCall = function (url, Data)
    {
        $http.defaults.headers.post["Content-Type"] = "application/json";
        result = $http.post(url, Data).success(function (data, status) {
            result = (data);
        }).error(function () {
            alert("Something went wrong");
        });
        return result;
    };

}]);

app.controller("airlineController", ['$scope', '$http', 'ApiCall', 'ModalService',
    function ($scope, $http, ApiCall, ModalService) {

        $scope.AddReq;
        $scope.isAddSuccess;
        $scope.init = function () {
            $scope.AddReq = null;
            $scope.isAddSuccess = null;
            $scope.OneWayTick = true;
            $scope.isSearch = false;
            $scope.isSearchFail = false;
            $scope.MessageSearchFail = "dadadss";
            $scope.MessageNoFlight = "dddd";
            $scope.isNoFlight = false;
            $scope.isNoFlightRet = false;

            var dtNow = new Date();
            dtNow.setHours(0, 0, 0, 0);
            $scope.SearchRequest =
            {
                DepartureDate: dtNow,
                ReturnDate: dtNow,
                Adult: 0,
                Children: 0,
                Infant: 0,
                TotalPassengers: 0,
            };

            //Lấy danh sách các sân bay đi
            $scope.OriginAirPorts;
            var url = window.location.origin + '/' + 'api/' + "System" + '/' + "v1" + '/' + "GetOrigin";
            var result1 = ApiCall.GetApiCall(url).success(function (data) {
                $scope.OriginAirPorts = data.Data;

                var urlDestination = window.location.origin + '/' + 'api/' + "System" + '/' + "v1" + '/' + "GetDestination?origincode=" + $scope.SearchRequest.AirPortOrigin;
                //Lấy danh sách các sân bay đến
                var result2 = ApiCall.GetApiCall(urlDestination).success(function (data) {
                    $scope.DestinationAirPorts = data.Data;
                    $scope.SearchRequest.AirPortDestination = angular.copy($scope.DestinationAirPorts[0].Code);
                });
            });

        };

        $scope.beforeRenderDepartureDate = function ($view, $dates, $leftDate, $upDate, $rightDate, $SearchReq, $DepartureDate, $ReturnDate) {
            var dtNow = new Date();
            dtNow.setHours(0, 0, 0, 0);
            for (var i = 0; i < $dates.length; i++) {
                if ($dates[i].localDateValue() < dtNow.valueOf()) {
                    $dates[i].selectable = false;
                }
                //if (moment(dtNow.valueOf()).isAfter($dates[i].localDateValue(), 'day'))
                //{
                //    $dates[i].selectable = false;
                //}
            }

            if (!$scope.OneWayTick) {
                if ($SearchReq[$DepartureDate] >= $SearchReq[$ReturnDate]) {
                    $SearchReq[$ReturnDate] = angular.copy($SearchReq[$DepartureDate]);
                }
                else {
                    $SearchReq[$ReturnDate] = angular.copy($SearchReq[$ReturnDate]);
                }
            }
        }

        $scope.beforeRenderReturnDate = function ($view, $dates, $leftDate, $upDate, $rightDate, $SearchReq, $DepartureDate, $ReturnDate) {
            var dtNow = new Date();
            dtNow.setHours(0, 0, 0, 0);
            for (var i = 0; i < $dates.length; i++) {
                if ($dates[i].localDateValue() < dtNow.valueOf() || $dates[i].localDateValue() < $SearchReq[$DepartureDate]) {
                    $dates[i].selectable = false;
                }
            }
        }

        $scope.onTimeSet = function (newDate, oldDate) {
            $("#dtPicker").click();
        }

        $scope.SetTime = function (date, hours, min, sec, mili) {
            date.setHours(hours);
            date.setMinutes(min);
            date.setSeconds(sec);
            date.setMilliseconds(mili);

        };

        $scope.OneWayClick = function () {
            $scope.OneWayTick = !$scope.OneWayTick;

            var btn = document.getElementById("btnOneWay");
            if (btn.innerHTML == "One Way") {
                btn.innerHTML = "Round Trip"
                return;
            }
            else (btn.innerHTML == "Round Trip")
            {
                btn.innerHTML = "One Way"
            }
        };

        $scope.AdtSubClick = function () {
            if ($scope.SearchRequest.Adult == 0) {
                return;
            }
            else {
                if ($scope.SearchRequest.Adult == 1) {
                    $scope.SearchRequest.Adult = 0;
                    $scope.SearchRequest.Children = 0;
                    $scope.SearchRequest.Infant = 0;
                    $scope.SearchRequest.TotalPassengers = 0;
                }
                else {
                    $scope.SearchRequest.Adult = $scope.SearchRequest.Adult - 1;
                    $scope.SearchRequest.TotalPassengers = $scope.SearchRequest.TotalPassengers - 1;
                }
            }
        };

        $scope.AdtPlusClick = function () {
            if ($scope.SearchRequest.TotalPassengers >= 6) {
                return;
            }
            else {
                $scope.SearchRequest.TotalPassengers = $scope.SearchRequest.TotalPassengers + 1;
                $scope.SearchRequest.Adult = $scope.SearchRequest.Adult + 1;
            }
        };

        $scope.ChildSubClick = function () {
            if ($scope.SearchRequest.Children == 0) {
                return;
            }
            else {
                $scope.SearchRequest.Children = $scope.SearchRequest.Children - 1;
                $scope.SearchRequest.TotalPassengers = $scope.SearchRequest.TotalPassengers - 1;
            }
        };

        $scope.ChildPlusClick = function () {
            if ($scope.SearchRequest.TotalPassengers >= 6) {
                return;
            }
            else {
                if ($scope.SearchRequest.Adult <= 0) {
                    return;
                }
                else {
                    $scope.SearchRequest.TotalPassengers = $scope.SearchRequest.TotalPassengers + 1;
                    $scope.SearchRequest.Children = $scope.SearchRequest.Children + 1;
                }
            }
        };

        $scope.InfSubClick = function () {
            if ($scope.SearchRequest.Infant == 0) {
                return;
            }
            else {
                $scope.SearchRequest.Infant = $scope.SearchRequest.Infant - 1;
                $scope.SearchRequest.TotalPassengers = $scope.SearchRequest.TotalPassengers - 1;
            }
        };

        $scope.InfPlusClick = function () {
            if ($scope.SearchRequest.TotalPassengers >= 6) {
                return;
            }
            else {
                if ($scope.SearchRequest.Adult <= 0) {
                    return;
                }
                else {
                    $scope.SearchRequest.TotalPassengers = $scope.SearchRequest.TotalPassengers + 1;
                    $scope.SearchRequest.Infant = $scope.SearchRequest.Infant + 1;
                }
            }

        };

        $scope.OriginChange = function () {
            //Khi chọn 1 nơi đi mới - thì sẽ gọi xuống api để lấy danh sách nơi đến
            var url = window.location.origin + '/' + 'api/' + "System" + '/' + "v1" + '/' + "GetDestination?origincode=" + $scope.SearchRequest.AirPortOrigin;
            //Lấy danh sách các sân bay đến
            var result2 = ApiCall.GetApiCall(url).success(function (data) {
                $scope.DestinationAirPorts = data.Data;
                $scope.SearchRequest.AirPortDestination = angular.copy($scope.DestinationAirPorts[0].Code);
                //ng-init="SearchRequest.AirPortDestination = DestinationAirPorts[0].Code"
            });
        };

        $scope.getStationName = function (Code) {
            if ($scope.OriginAirPorts != null) {
                var n = $scope.OriginAirPorts.length;
                for (var i = 0; i < n; i++) {
                    if (angular.equals($scope.OriginAirPorts[i].Code, Code)) {
                        return $scope.OriginAirPorts[i].Name;
                    }
                }
            }
        };

        $scope.clearFilter = function () {
            $('.footable').trigger('footable_clear_filter');
        };

        $scope.DateToString = function (date) {
            var mm = (date.getMonth() + 1).toString(); // getMonth() is zero-based
            var dd = (date.getDate()).toString();
            var yy = (date.getFullYear()).toString();

            if (mm < 10) {
                mm = '0' + mm.toString();
            }
            if (dd < 10) {
                dd = '0' + dd.toString();
            }

            return [yy, mm, dd].join('');
            //return [date.getFullYear(), !mm[1] && '0', mm, !dd[1] && '0', dd].join(''); // padding
        };

        $scope.getDetail = function (item) {
            var model = ModalService.showModal({
                templateUrl: '../View/Detail.html',
                controller: "detailController",
                inputs: {
                    Data: item,
                    Req: $scope.req,
                },
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //$scope.yesNoResult = result ? alert("Bạn đã đặt hàng thành công") : null;
                    $scope.yesNoResult = result;
                });
            });
        };

        $scope.Search = function () {

            $scope.hsDepart = null;
            $scope.hsReturn = null;

            if ($scope.SearchRequest.TotalPassengers == 0) {
                $scope.isSearchFail = true;
                $scope.MessageSearchFail = "Số lượng hành khách không phù hợp với điều kiện search";
                return;
            }

            $scope.req = angular.copy($scope.SearchRequest);
            var paxCountCode = ($scope.req.Adult * 100) + ($scope.req.Children * 10) + $scope.req.Infant;
            var strDepartDate = $scope.DateToString($scope.req.DepartureDate);
            var strReturnDate = null;
            var urlSearch = window.location.origin + '/' + 'api/' + "Ticket" + '/' + "v1" + '/'
                + $scope.req.AirPortOrigin + '/' + $scope.req.AirPortDestination + '/'
                + paxCountCode.toString() + '/' + strDepartDate;
            if (!$scope.OneWayTick) {
                strReturnDate = $scope.DateToString($scope.req.ReturnDate);
                urlSearch = urlSearch + '/' + strReturnDate;
            }


            //Gọi API Search
            //http://localhost:3516/Api/Ticket/v1/SGN/HAN/321/20161119/20161125

            //Lấy danh sách các sân bay đến
            var result2 = ApiCall.GetApiCall(urlSearch).success(function (data) {
                $scope.hsDepart = data.Data.Depart;
                $scope.hsReturn = data.Data.mReturn;

                if ($scope.hsDepart == null || $scope.hsDepart.length == 0) {
                    $scope.isNoFlight = true;
                    $scope.MessageNoFlight = "Không có chuyến bay nào phù hợp";
                }

                if ($scope.hsReturn == null || $scope.hsReturn.length == 0) {
                    $scope.isNoFlightRet = true;
                    $scope.MessageNoFlightRet = "Không có chuyến bay nào phù hợp";
                }
                

            });

            $scope.isSearchFail = false;
            $scope.isSearch = true;
            $scope.isNoFlight = false;
            $scope.isNoFlightRet = false;
        };

        $scope.init();
    }]);

app.controller("detailController", ['$scope', '$http', 'ApiCall', 'close', 'Data', 'Req',
function ($scope, $http, ApiCall, close, Data, Req) {

    $scope.init = function ()
    {
        $scope.info = angular.copy(Data);
        $scope.req = angular.copy(Req);
        $scope.Male = true;
        $scope.Female = false;
        $scope.IsBookSuccess = false;
        $scope.IsBookingFail = false;
        $scope.ReqModel = {
            Name: "",
            Phone: "",
            Email: "",
            Type:"MR"
        };
    };

    $scope.close = function (result) {
        
        close(result, 3000); // close, but give 500ms for bootstrap to animate
    };

    $scope.Book = function ()
    {
        if (!$scope.ReqModel.Name.trim() || !$scope.ReqModel.Phone.trim() || !$scope.ReqModel.Email.trim()) {
            //do nothing
            $scope.IsBookingFail = true;
            return;
        }
        else {
            
            $scope.BookingReq =
                {
                    Request: {
                        Departure: {
                            flightNumber: $scope.info.FlightID.trim(),
                            departureTime: $scope.info.FlightTime.trim(),
                            seatClass: $scope.info.SeatClass.trim(),
                            priceClass: $scope.info.PriceClass.trim(),
                            priceTotal: $scope.info.Price * $scope.req.TotalPassengers
                        },
                        Return:null
                    },
                    PassengerInfo: {
                        Name: $scope.ReqModel.Name,
                        Phone: $scope.ReqModel.Phone,
                        email: $scope.ReqModel.Email,
                        Type:$scope.ReqModel.Type
                    }
                };

            //Api/Ticket/v1/book
            var url = window.location.origin + '/' + 'api/' + "Ticket" + '/' + "v1" + '/' + "book";
            //var json = JSON.stringify($scope.Req);
            var result2 = ApiCall.PostApiCall(url, $scope.BookingReq).success(function (data) {
                $scope.Data = angular.copy(data.Data);

                if ($scope.Data.code == 200) {
                    $scope.IsBookSuccess = true;
                    $scope.IsBookingFail = false;
                }
                else
                {
                    $scope.IsBookingFail = true;
                    $scope.IsBookSuccess = false;
                }

            });
        }
    };

    $scope.MaleClick = function ()
    {
        $scope.Male = true;
        $scope.Female = false;
        $scope.Req.Type = "MR";
    };

    $scope.FemaleClick = function ()
    {
        $scope.Male = false;
        $scope.Female = true;
        $scope.Req.Type = "MS";
    };

    $scope.init();
}]);