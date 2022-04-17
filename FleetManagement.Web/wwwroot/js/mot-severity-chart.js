// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.font.family = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.color = '#858796';

var motSeverityCallBack = function (response)
{
    var outcomes = [];
    var outcomesCount = [];

    for (const outcome in response) {
        outcomes.push(outcome);
        outcomesCount.push(response[outcome]);
    }

    // Pie Chart Example
    var ctx = document.getElementById("motSeverityBarChart");
    var motSeverityBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: outcomes,
            datasets: [{
                data: outcomesCount,
                backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#e8ed4c', '#d98b2b', '#32a4a8', '#4cc746'],
                hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#969936', '#7a490c', '#2b6b6e', '#356b32'],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
            }],
        },
        options: {
            scales: {
                y: {
                    ticks: {
                        precision: 0
                    }
                },
            },            
            plugins: {
                legend: {
                    display: false
                }
            },
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
            }
        }
    });
}

$.ajax("/api/Dashboard/MOTSeverity", {
    success: (response) => motSeverityCallBack(response)
});