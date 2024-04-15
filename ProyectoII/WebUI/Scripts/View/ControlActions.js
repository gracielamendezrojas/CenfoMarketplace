if (localStorage.getItem("UserProfile") === null) {
	document.querySelector(".navbar-nav").classList.add("hidden");
	document.querySelector("#logout-button").classList.add("hidden");
}
else {
	document.querySelector(".navbar-nav").classList.remove("hidden");
	document.querySelector("#logout-button").classList.remove("hidden");
}

document.querySelector('#logout-button').addEventListener('click', () => {
	localStorage.removeItem("UserProfile");
	localStorage.removeItem("UserRoles");

});


function ControlActions() {

	this.URL_API = "https://cenfo-api.azurewebsites.net/api/";
	this.FE = "https://cenfomarket.azurewebsites.net/"; 
	//this.URL_API = "https://localhost:44395/api/";
	//this.FE = "https://localhost:44363/"
	

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.GetTableColumsDataName = function (tableId) {
		var val = $('#' + tableId).attr("ColumnsDataName");

		return val;
	}

	this.FillTable = function (service, tableId ,refresh) {

		if (!refresh) {
			columns = this.GetTableColumsDataName(tableId).split(',');
			var arrayColumnsData = [];


			$.each(columns, function (index, value) {
				var obj = {};
				obj.data = value;
				arrayColumnsData.push(obj);
			});
			//Esto es la inicializacion de la tabla de data tables segun la documentacion de 
			// datatables.net, carga la data usando un request async al API
			$('#' + tableId).DataTable({
				"processing": true,
				"ajax": {
					"url": this.GetUrlApiService(service),
					dataSrc: 'Data'
				},
				"columns": arrayColumnsData
			});
		} else {
			//RECARGA LA TABLA
			$('#' + tableId).DataTable().ajax.reload();
		}
		
	}

	this.GetSelectedRow = function () {
		var data = sessionStorage.getItem(tableId + '_selected');

		return data;
	};

	this.BindFields = function (formId, data) {
		console.log(data);
		$('#' + formId +' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			this.value = data[columnDataName];
		});
	}

	this.GetDataForm = function (formId) {
		var data = {};
		
		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			data[columnDataName] = this.value;
		});

		console.log(data);
		return data;
	}

	this.ShowMessage = function (type,message) {
		if (type == 'E') {
			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text(message);
		} else if (type == 'I') {
			$("#alert_container").removeClass("alert alert-danger alert-dismissable")
			$("#alert_container").addClass("alert alert-success alert-dismissable");
			$("#alert_message").text(message);
		}
		$('.alert').show();
	};

	this.PostToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);

			if (callBackFunction) {
				callBackFunction(response);
				return response;
            }
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.PutToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}

		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.DeleteToAPI = function (service, data,callbackFunction) {
		var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
    };

    this.GetToApi = function (service, callbackFunction) {
		var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
			console.log("Response " + response);
			if (callBackFunction) {
				callbackFunction(response.Data);
			}
            
        });
    }
}

//Custom jquery actions
$.put = function (url, data, callback) {
	if ($.isFunction(data)) {
		type = type || callback,
			callback = data,
			data = {}
	}
	return $.ajax({
		url: url,
		type: 'PUT',
		success: callback,
		data: JSON.stringify(data),
		contentType: 'application/json'
	});
}

$.delete = function (url, data, callback) {
	if ($.isFunction(data)) {
		type = type || callback,
			callback = data,
			data = {}
	}
	return $.ajax({
		url: url,
		type: 'DELETE',
		success: callback,
		data: JSON.stringify(data),
		contentType: 'application/json'
	});
}


$(document).ready(function () {
	if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 1) {
		document.querySelector(".dashboard-link");
		console.log(document.querySelector(".dashboard-link").innerHTML);
		$(".dashboard-link").attr("href", new ControlActions().FE +"dashboard/admindashboard");
	}
	else if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 2) {
		$(".dashboard-link").attr("href", new ControlActions().FE +"dashboard/buyerdashboard");
	}
	else if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 3) {
		$(".dashboard-link").attr("href", new ControlActions().FE +"dashboard/creatordashboard");
	}
	if (localStorage.getItem("UserProfile") === null) {
		console.log("brrr")
		$(".navbar-nav").addClass("hidden");
	}
});
