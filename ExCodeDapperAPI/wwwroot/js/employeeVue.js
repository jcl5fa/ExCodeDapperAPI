const employeeVue = {
    template: `
<div>
    <button type="button" class="btn btn-primary m-2 fload-end"
    data-bs-toggle="modal" data-bs-target="#exampleModal"
    @click="addClick()">
        Add Employee
    </button>

    <table class="table table-striped">
    <thead>
        <tr>
            <th>
                Employee Id
            </th>
            <th>
                Employee First Name
            </th>
            <th>
                Employee Last Name
            </th>
            <th>
                Department Name
            </th>
            <th>
                Manager Name
            </th>
            <th>
                Actions
            </th>
        </tr>
    </thead>
    <tbody>
        <tr v-for="emp in employees">
            <td>{{emp.id}}</td>
            <td>{{emp.firstName}}</td>
            <td>{{emp.lastName}}</td>
            <td>{{emp.department.name}}</td>
            <td>{{emp.manager.firstName + " " + emp.manager.lastName}}</td>
            <td>
                <button type="button" class="btn btn-light mr-1"
                data-bs-toggle="modal" data-bs-target="#exampleModal"
                    @click="editClick(emp)">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                        <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                    </svg>
                </button>
                <button type="button" class="btn btn-light mr-1" @click="deleteClick(emp.id)">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                        <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                    </svg>
                </button>
            </td>
        </tr>
    </tbody>
    
    </table>
    <!-- modal popup -->
    <div class="modal fade" id="exampleModal" tabindex="-1"
        aria-labelledby="exampleModelLabel" aria-hidden="true">
        
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
                    <!-- not working for some odd reason
                    <button type="button" class="btn-close" data-bs-dimiss="modal" aria-label="Close"></button>
                    -->
                </div>

                <div class="modal-body">
                    <div class="input-group mb-3">
                        <span class="input-group-text">Employee First Name</span>
                        <input type="text" class="form-control" v-model="EmployeeFirstName">
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text">Employee Last Name</span>
                        <input type="text" class="form-control" v-model="EmployeeLastName">
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text">Department</span>                        
                        <select v-model="DepartmentId" id="departmentList" class="form-control">
                            <option v-for="dept in departments" v-bind:value="dept.id">
                                {{dept.name}}
                            </option>
                        </select>
                    </div>
                    
                    <div class="input-group mb-3">
                        <span class="input-group-text">Manager</span>                        
                        <select v-model="ManagerId" id="managerList" class="form-control">
                            <option v-for="mgr in managers" v-bind:value="mgr.id">
                                {{mgr.firstName + " " + mgr.lastName}}
                            </option>
                        </select>
                    </div>

                    <button type="button" v-if="EmployeeId==0" class="btn btn-primary" @click="createClick()">
                        Create
                    </button>
                    <button type="button" v-if="EmployeeId!=0" class="btn btn-primary" @click="updateClick()">
                        Update
                    </button>
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</div>
`,

    data() {
        return {
            employees: [],
            modalTitle: "",
            EmployeeFirstName: "",
            EmployeeLastName: "",
            EmployeeId: 0,
            ManagerId: 0,
            DepartmentId: 0,
            managers: [],
            departments: []

        }
    },
    methods: {
        refreshData() {
            axios.get(variables.API_URL + "employee")
                .then((response) => {
                    this.employees = response.data;
                    console.log(response);
                });

            axios.get(variables.API_URL + "manager")
                .then((response) => {
                    this.managers = response.data;
                    console.log(response);
                });

            axios.get(variables.API_URL + "department")
                .then((response) => {
                    this.departments = response.data;
                    console.log(response);
                });
        },
        addClick() {
            this.modalTitle = "Add Employee";
            this.EmployeeId = 0;
            this.EmployeeFirstName = "";
            this.EmployeeLastName = "";
            this.DepartmentId = 0;
            this.ManagerId = 0;
        },
        editClick(emp) {
            this.modalTitle = "Edit Employee";
            this.EmployeeId = emp.id;
            this.EmployeeFirstName = emp.firstName;
            this.EmployeeLastName = emp.lastName;
            this.DepartmentId = emp.department.id;
            this.ManagerId = emp.manager.id;
        },
        createClick() {
            axios.post(variables.API_URL + "employee", {
                firstName: this.EmployeeFirstName,
                lastName: this.EmployeeLastName,
                departmentId: this.DepartmentId,
                managerId: this.ManagerId

            })
                .then((response) => {
                    this.employees = response.data;
                });
        },
        updateClick() {
            axios.put(variables.API_URL + "employee", {
                id: this.EmployeeId,
                firstName: this.EmployeeFirstName,
                lastName: this.EmployeeLastName,
                departmentId: this.DepartmentId,
                managerId: this.ManagerId
            })
                .then((response) => {
                    this.employees = response.data;
                });
        },
        deleteClick(id) {
            if (!confirm("Are you sure?")) {
                return 
            }
            axios.delete(variables.API_URL + "employee/" + id)
                .then((response) => {
                    this.employees = response.data;
                });
        }
    },
    mounted: function () {
        this.refreshData();
    }
}