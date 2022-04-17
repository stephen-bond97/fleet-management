// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.font.family = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.color = '#858796';

var vehicleTypesCallBack = function (response)
{
    var bodyTypes = [];
    var bodyTypesCount = [];

    for (const bodyType in response) {
        bodyTypes.push(bodyType);
        bodyTypesCount.push(response[bodyType]);
    }

    // Pie Chart Example
    var ctx = document.getElementById("vehicleTypesPieChart");
    var vehicleTypesPieChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: bodyTypes,
            datasets: [{
                data: bodyTypesCount,
                backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#e8ed4c', '#d98b2b', '#32a4a8', '#4cc746'],
                hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#969936', '#7a490c', '#2b6b6e', '#356b32'],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
            }],
        },
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretPadding: 10,
            },
            legend: {
                display: false
            },
            cutoutPercentage: 80,
        },
    });
}

$.ajax("/api/Dashboard/VehicleTypes", {
    success: (response) => vehicleTypesCallBack(response)
});