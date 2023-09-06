$(document).ready(() => {
    getVacationTypes();
})


findEmployee = () => {
    if ($('#employeeName').val() == '') {
        $('#ddlEmployeeId').html('<option value="">-----------No Items Found----------</option>')
    }
    else {
        $.ajax({
            url: '/api/VacationPlanApi/' + $('#employeeName').val(),
            Method: 'GET',
            cache: false,
            success: (data) => {
                let employee = '';

                employee += `<option value="">-----------ItemsFound (${data.length})----------</option>`;

                for (x in data) {
                    employee += `<option value="${data[x].id}">${data[x].name}</option>`
                }

                $('#ddlEmployeeId').html(employee);
            }

        })
    }
    
}

getVacationTypes = () => {
    $.ajax({
        url: '/VacationPlans/GetVacationTypes',
        Method: 'GET',
        cache: false,
        success: (result) => {
            let vacations = '';

            vacations += `<option value="">-----------Select Vacation (${result.length})----------</option>`;

            for (x in result) {
                vacations += `<option value="${result[x].id}" style="color:#ffff; background-color:${result[x].backgroundColor};">${result[x].vacationName} ------Days:-> (${result[x].numberDays})</option>`;
            }

            $('#ddlVacationType').html(vacations);
        }
    })
}