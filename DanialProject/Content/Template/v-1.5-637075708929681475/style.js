var Filter = "Rent";
$(document).ready(function () {
    $("#preloader").hide();
    $('.close-mobile-navbar').on('click', function (e) {
        $(this).closest('.top-navbar-mobile').removeClass('show-mobile-navbar');
    });
    $('.mobile-nav-opener').on('click', function (e) {
        $(this).closest('.navbar').find('.top-navbar-mobile').addClass('show-mobile-navbar');
    });
});

//Function to return FilterBox Cookie Data
var code = (function () {
    return {
        getCookie: function (name) {
            var value = "; " + document.cookie;
            var parts = value.split("; " + name + "=");
            if (parts.length === 2) {
                return parts.pop().split(";").shift();
            }
        }
    };
})();

//Cookie Data as List
function getCookieData() {
    var data = code.getCookie('FilterBoxCookiesInDallienWebsite');
    var split = data.split("&");
    return [split[0], split[1], split[2], split[3], split[4]];
}

//Set Data in Filter Box
function SetFilterBox() {
    //Getting Cookie Data
    var data = getCookieData();

    document.getElementById("label-price").innerHTML = data[1] + " - " + data[2];
    document.getElementById("from-price").value = data[1];
    document.getElementById("to-price").value = data[2];

    document.getElementById("label-room").innerHTML = data[3] + " BED / " + data[4] + " BATH";
    document.getElementById("bed").value = data[3];
    document.getElementById("bath").value = data[4];

    $('.category-tabs').removeClass('selected-rent');
    $('.category-tabs').removeClass('selected-buy');
    $('.category-tabs').removeClass('selected-room');

    if (data[0] === "Rent") {
        $('.rent').addClass('selected-rent');
        document.getElementById("Filter").value = "Rent";

        $('#filter-search').addClass('btn-selected-rent');
        $('#filter-search').removeClass('btn-selected-buy');
        $('#filter-search').removeClass('btn-selected-room');
    }
    else if (data[0] === "Sale") {
        $('.buy').addClass('selected-buy');
        document.getElementById("Filter").value = "Sale";

        $('#filter-search').removeClass('btn-selected-rent');
        $('#filter-search').addClass('btn-selected-buy');
        $('#filter-search').removeClass('btn-selected-room');
    }
    else if (data[0] === "find_a_room") {
        $('.find-room').addClass('selected-room');
        document.getElementById("Filter").value = "find_a_room";

        $('#filter-search').addClass('btn-selected-rent');
        $('#filter-search').removeClass('btn-selected-buy');
        $('#filter-search').addClass('btn-selected-room');
    }
    else {
        $(this).addClass('selected-rent');
        document.getElementById("Filter").value = "Rent";

        $('#filter-search').addClass('btn-selected-rent');
        $('#filter-search').removeClass('btn-selected-buy');
        $('#filter-search').removeClass('btn-selected-room');
    }
}

//Set Data in Session for FilterBox
function SetSessionForFilterBox(filter) {
    $("#preloader").show();
    var Login = {
        Filter: $("#Filter").val(), From_price: $("#from-price").val(),
        To_price: $("#to-price").val(), Bed: $("#bed").val(),
        Bath: $("#bath").val()
    };
    var LoginData = JSON.stringify(Login);
    // Make Ajax request with the contentType = false, and procesDate = false
    var ajaxRequest = $.ajax({
        type: "POST",
        url: "/api/WebApis/SetSession",
        contentType: 'application/json; charset=utf-8',
        processData: false,
        dataType: "json",
        data: LoginData,
        success: function (data, textStatus, xhr) {
            if (data === 0) {
                bootbox.alert('There was an error. try again later.');
                $("#preloader").hide();
            }
            else {
                if (filter === "Map") {
                    initMap();
                }
                else if (filter === "SimpleListing") {
                    window.location = "../../Home/SimpleListing";
                }
                else if (filter === "FindListing") {
                    window.location = "../../Home/FindListing?filter=Repeat";
                }
                else {
                    RefreshPage();
                }
            }
        },
        error: function (err, textStatus, errorThrown) {
            bootbox.alert('There was an error. try again later.');
            $("#preloader").hide();
        }
    });
    return false;
}

//HomePage
function HomePage() {
    location.href = '../../Home/Index';
}

//Form For New Record
function LoginForm() {
    $("#preloader").show();
    document.getElementById("btnLogin").disabled = true;
    var Login = {
        Email: $("#LoginEmail").val(), Password: $("#LoginPassword").val()
    };
    var LoginData = JSON.stringify(Login);
    // Make Ajax request with the contentType = false, and procesDate = false
    var ajaxRequest = $.ajax({
        type: "POST",
        url: "/api/WebApis/CheckLogin",
        contentType: 'application/json; charset=utf-8',
        processData: false,
        dataType: "json",
        data: LoginData,
        success: function (data, textStatus, xhr) {
            if (data === 0) {
                document.getElementById("btnLogin").disabled = false;
                document.getElementById("ErrorLogin").innerHTML = "Username or password is Incorrect!";
                $("#preloader").hide();
            }
            else {
                window.location.reload();
            }
        },
        error: function (err, textStatus, errorThrown) {
            document.getElementById("btnLogin").disabled = false;
            document.getElementById("ErrorLogin").innerHTML = "Username or password is Incorrect!";
            $("#preloader").hide();
        }
    });
    return false;
}

//For Search Filter
function U_BedsInc(id) {
    var str = id.includes("+");
    if (str) {
        str = id.replace('+', '');
        var value = parseFloat(str);
        if (id <= 0) {
            $('#bed').val("0+");
        }
        else {
            value = value + 1;
            $('#bed').val(value);
        }
    }
    else {
        $('#bed').val(id + "+");
    }
    label_Room_Change();
}

//For Search Filter
function U_Bedsdec(id) {
    var str = id.includes("+");
    if (str) {
        str = id.replace('+', '');
        var value = parseFloat(str);
        if (id <= 0) {
            $('#bed').val("0+");
        }
        else {
            $('#bed').val(value);
        }
    }
    else {
        if (id <= 0) {
            $('#bed').val("0");
        }
        else {
            value = parseFloat(id);
            value = value - 1;
            $('#bed').val(value + "+");
        }
    }
    label_Room_Change();
}

//For Search Filter
function U_BathsInc(id) {
    var str = id.includes("+");
    if (str) {
        str = id.replace('+', '');
        var value = parseFloat(str);
        if (id <= ".5") {
            $('#bath').val(".5+");
        }
        if (id <= ".5+") {
            $('#bath').val("1");
        }
        else {
            value = value + 1;
            $('#bath').val(value);
        }
    }
    else {
        if (id <= 0) {
            $('#bath').val(".5");
        }
        else {
            $('#bath').val(id + "+");
        }
    }
    label_Room_Change();
}

//For Search Filter
function U_Bathsdec(id) {
    var str = id.includes("+");
    if (str) {
        str = id.replace('+', '');
        var value = parseFloat(str);
        if (id <= 0) {
            $('#bath').val("0+");
        }
        else {
            if (value === 0.5) {
                $('#bath').val(".5");
            }
            else {
                $('#bath').val(value);
            }
        }
    }
    else {
        if (id <= 1) {
            if (id === "1") {
                $('#bath').val(".5+");
            }
            else {
                $('#bath').val("0");
            }
        }
        else {
            value = parseFloat(id);
            value = value - 1;
            $('#bath').val(value + "+");
        }
    }
    label_Room_Change();
}

//For Sale And Rentals
function BedsInc(id) {
    if (id <= 0) {
        $('#bed').val(1);
    }
    else {
        $('#bed').val(parseFloat(id) + 0.5);
    }
}

//For Sale And Rentals
function Bedsdec(id) {
    if (id <= 1) {
        $('#bed').val(0);
    }
    else {
        $('#bed').val(id - 0.5);
    }
}

//For Sale And Rentals
function BathsInc(id) {
    $('#bath').val(parseFloat(id) + 0.5);
}

//For Sale And Rentals
function Bathsdec(id) {
    if (id <= 0) {
        $('#bath').val(0);
    }
    else {
        $('#bath').val(id - 0.5);
    }
}

$(document).ready(function () {
    $('.category-tabs').on('click', function (e) {
        try {
            $('.category-tabs').removeClass('selected-rent');
            $('.category-tabs').removeClass('selected-buy');
            $('.category-tabs').removeClass('selected-room');

            if ($(this).hasClass('rent')) {
                $(this).addClass('selected-rent');
                document.getElementById("label-price").innerHTML = "$0 - $10,000";
                document.getElementById("from-price").value = "$0";
                document.getElementById("to-price").value = "$10,000";

                document.getElementById("Filter").value = "Rent";

                $('#filter-search').addClass('btn-selected-rent');
                $('#filter-search').removeClass('btn-selected-buy');
                $('#filter-search').removeClass('btn-selected-room');
            }
            else if ($(this).hasClass('buy')) {
                $(this).addClass('selected-buy');
                document.getElementById("label-price").innerHTML = "$0 - $25,000,000";
                document.getElementById("from-price").value = "$0";
                document.getElementById("to-price").value = "$25,000,000";

                document.getElementById("Filter").value = "Sale";

                $('#filter-search').removeClass('btn-selected-rent');
                $('#filter-search').addClass('btn-selected-buy');
                $('#filter-search').removeClass('btn-selected-room');
            }
            else if ($(this).hasClass('find-room')) {
                $(this).addClass('selected-room');
                document.getElementById("label-price").innerHTML = "$0 - $4,000";
                document.getElementById("from-price").value = "$0";
                document.getElementById("to-price").value = "$4,000";

                document.getElementById("Filter").value = "find_a_room";

                $('#filter-search').addClass('btn-selected-rent');
                $('#filter-search').removeClass('btn-selected-buy');
                $('#filter-search').addClass('btn-selected-room');
            }
            else {
                $(this).addClass('selected-rent');
                document.getElementById("label-price").innerHTML = "$0 - $10,000";
                document.getElementById("from-price").value = "$0";
                document.getElementById("to-price").value = "$10,000";

                document.getElementById("Filter").value = "Rent";

                $('#filter-search').addClass('btn-selected-rent');
                $('#filter-search').removeClass('btn-selected-buy');
                $('#filter-search').removeClass('btn-selected-room');
            }
        } catch (error) { console.log("Error"); }
    });
});

//This Fuction is for calulation of price in search filters
function label_Price_Change() {
    $('#from-price').priceFormat({
        prefix: '$',
        suffix: '',
        centsLimit: 0
    });

    $('#to-price').priceFormat({
        prefix: '$',
        suffix: '',
        centsLimit: 0
    });

    var from_price = document.getElementById("from-price").value;
    var to_price = document.getElementById("to-price").value;

    if (from_price === "$") {
        from_price = "$0";
    }
    else if (from_price.includes("$0")) {
        if (from_price.length > 2) {
            from_price = from_price.replace("$0", "$");
        }
    }
    else if (from_price === "") {
        from_price = "$0";
    }

    if (to_price === "$") {
        to_price = "$0";
    }
    else if (to_price.includes("$0")) {
        if (to_price.length > 2) {
            to_price = to_price.replace("$0", "$");
        }
    }
    else if (to_price === "") {
        to_price = "$0";
    }


    var temp_from_price = from_price.replace("$", "");
    temp_from_price = temp_from_price.replace(/,/g, '');

    var temp_to_price = to_price.replace("$", "");
    temp_to_price = temp_to_price.replace(/,/g, '');

    var from_price_Int = parseInt(temp_from_price);
    var to_price_Int = parseInt(temp_to_price);

    if (from_price_Int > to_price_Int) {
        from_price = to_price;
        document.getElementById("from-price").value = from_price;
        document.getElementById("to-price").value = to_price;
    }

    document.getElementById("label-price").innerHTML = from_price + " - " + to_price;
}

//This Fuction is for chnaging of Room in search filters
function label_Room_Change() {
    document.getElementById("label-room").innerHTML = document.getElementById("bed").value + " BED / " + document.getElementById("bath").value + " BATH";
}

function ForgotPassword() {
    var Email = $("#LoginEmail").val();
    if ((Email !== "" || Email !== null) && Email.includes("@")) {
        $("#preloader").show();
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/api/WebApis/ForgotButton?email=" + Email + "",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            success: function (data, textStatus, xhr) {
                if (data === 0) {
                    document.getElementById("ForgotButton").disabled = false;
                    document.getElementById("ErrorLogin").innerHTML = "Email provided doesn't exist in dallien.com";
                    $("#preloader").hide();
                }
                else {
                    $("#LoginModal").modal('hide');
                    $("#ChnagePassword").modal();
                    $("#preloader").hide();
                }
            },
            error: function (err, textStatus, errorThrown) {
                document.getElementById("ForgotButton").disabled = false;
                document.getElementById("ErrorLogin").innerHTML = "Try Again Later!";
                $("#preloader").hide();
            }
        });
        return false;
    }
    else {
        document.getElementById("ErrorLogin").innerHTML = "You must enter your dallien.com email above.";
        return false;
    }
}

//Form For New Record
function UpdateLoginForm() {
    if ($("#DallienPassowrd").val() === $("#ConfirmPassword").val()) {
        $("#preloader").show();
        document.getElementById("btnLoginUpdate").disabled = true;
        var Login = {
            Email: $("#DallienEmail").val(), Password: $("#DallienPassowrd").val()
        };
        var LoginData = JSON.stringify(Login);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/api/WebApis/UpdatePassword?code=" + $("#DallienSecurityCode").val() + "",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            data: LoginData,
            success: function (data, textStatus, xhr) {
                if (data === 0) {
                    document.getElementById("btnLoginUpdate").disabled = false;
                    document.getElementById("ErrorLoginUpdate").innerHTML = "Username or Security Code is Incorrect!";
                    $("#preloader").hide();
                }
                else {
                    window.location.reload();
                }
            },
            error: function (err, textStatus, errorThrown) {
                document.getElementById("btnLoginUpdate").disabled = false;
                document.getElementById("ErrorLoginUpdate").innerHTML = "Try Again Later!";
                $("#preloader").hide();
            }
        });
        return false;
    }
    else {
        document.getElementById("ErrorLoginUpdate").innerHTML = "New Password and Confirm Password must be same.";
        return false;
    }
}


function ShowField_Price(_this) {
    $(_this).next().slideToggle("fast");

    if ($("#filter-card").hasClass("filter-card-Scroll-Price")) {
        setTimeout(function () {
            $("#filter-card").removeClass("filter-card-Scroll-Price");
            if (!$("#filter-card").hasClass("filter-card-Scroll-Rooms")) {
                $(".buy").removeClass("buy-toggle");
                $(".find-room").removeClass("find-room-toggle");
            }
        }, 200);
    }
    else {
        $("#filter-card").addClass("filter-card-Scroll-Price");
        $(".buy").addClass("buy-toggle");
        $(".find-room").addClass("find-room-toggle");
    }
}

function ShowField_Rooms(_this) {
    $(_this).next().slideToggle("fast");

    if ($("#filter-card").hasClass("filter-card-Scroll-Rooms")) {
        setTimeout(function () {
            $("#filter-card").removeClass("filter-card-Scroll-Rooms");
            if (!$("#filter-card").hasClass("filter-card-Scroll-Price")) {
                $(".buy").removeClass("buy-toggle");
                $(".find-room").removeClass("find-room-toggle");
            }
        }, 200);
    }
    else {
        $("#filter-card").addClass("filter-card-Scroll-Rooms");
        $(".buy").addClass("buy-toggle");
        $(".find-room").addClass("find-room-toggle");
    }
}

function CopyLink(id) {
    document.getElementById("CopyToClipboard").value = "https://www.dallien.com/Home/SingleListing/" + id;
}

function CopyLinkToClipbord() {
    document.getElementById("CopyToClipboard").style.display = "initial";

    var copyText = document.getElementById("CopyToClipboard");
    copyText.select();
    copyText.setSelectionRange(0, 99999);
    document.execCommand("copy");

    var dialog = bootbox.dialog({
        message: '<p class="text-center">Link Copied to Clipboard</p>',
        closeButton: false
    });

    document.getElementById("CopyToClipboard").style.display = "none";
    setTimeout(function () {
        dialog.modal('hide');
    }, 1000);
}

function dotDivider(value) {
    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
    });
    value = formatter.format(value);
    var splitdot = value.split(".");
    var toIntValue = parseInt(splitdot[1]);
    if (toIntValue > 0) {
        return value;
    }
    else {
        value = splitdot[0];
        return value;
    }
}
function Checkdot(value) {
    try {
        if (value.toString().includes(".")) {
            return value;
        } else {
            value = value + ".00";
            return value;
        }
    }
    catch (error) {
        value = value + ".00";
        return value;
    }
}
//Cookie Data as List
function getCookieDataFilterFeatures() {
    var data = code.getCookie('FilterFeaturesDeatils');
    var split = data.split("&");
    return [split[0], split[1], split[2], split[3], split[4], split[5], split[6], split[7], split[8], split[9]];
}

//Set Data in FilterFeatures Box
function SetFilterFeaturesBox() {
    //Getting Cookie Data
    var data = getCookieDataFilterFeatures();

    $('#Brough').select2().val(data[0]).trigger('change');

    document.getElementById("guarantor-accepted").value = data[4];
    document.getElementById("passenger-elevators").value = data[5];
    document.getElementById("frieght-elevators").value = data[6];

    var array = data[7].split(",");
    for (var i = 0; i < array.length - 1; i++) {
        document.getElementById(array[i]).checked = array[i];
    }

    var arr = data[1].split(",");
    $('#Neighborhood').select2().val(arr).trigger('change');


    try {
        document.getElementById("no-fee").checked = data[2].includes('True') ? true : false;
        document.getElementById("casebycase-ckb").checked = data[8].includes('True') ? true : false;
        document.getElementById("under30lbs-ckb").checked = data[9].includes('True') ? true : false;
        var res = data[3].split(" ");
        var date = res[0].split("/");
        document.getElementById("availibilty-date").value = date[2] + "-" + date[0] + "-" + date[1];
        PetsCheck();
    } catch (error) { var exe = error; }

}