const routes = [
    { path: '/home', component: homeVue },
    { path: '/department', component: departmentVue },
    { path: '/employee', component: employeeVue },
    { path: '/manager', component: managerVue }    
]

//https://v3.router.vuejs.org/installation.html#direct-download-cdn
Vue.use(VueRouter)

const router = new VueRouter({
    routes
})

const app = new Vue({
    router
}).$mount('#app')

//const router = VueRouter.createRouter({
//    routes
//})

//const app = Vue.createApp({})
//app.use(router)
//app.mount('#app')