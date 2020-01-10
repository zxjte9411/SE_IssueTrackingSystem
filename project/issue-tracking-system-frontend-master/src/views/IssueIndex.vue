<template>
    <div>
        <NavBar />
        <div class="container mt-4 ml-0">
            <div class="row justify-content-start">
                <div class="col col-auto">
                    <input class="form-control searchbar" type="text" placeholder="請輸入關鍵字" v-model="searchText">
                </div>
                <div class="col col-auto ml-0">
                    <button class="btn btn-primary" @click.prevent="openAddModal()">新增問題</button>
                </div>
            </div>
        </div>
        <table class="table mt-4 table-striped">
            <thead>
                <th style="width: 3%" scope="col">查看</th>
                <th style="width: 5%" scope="col">代號</th>
                <th style="width: 10%" scope="col">標題</th>
                <th style="width: 7%; min-width: 105px" scope="col">被指派者</th>
                <th style="width: 7%; min-width: 105px" scope="col">回報者</th>
                <th style="width: 13%" scope="col">狀態</th>
                <th style="width: 7%" scope="col">創立者</th>
                <th style="width: 3%" scope="col">刪除</th>
            </thead>
            <tbody>
                <tr v-for="(item, id) in handleIssues" :key="id">
                    <td class="align-middle">
                        <button type="button" class="btn btn-outline-secondary btn-sm"
                            @click.prevent="checkIssue(item)">
                            <font-awesome-icon icon="search" />
                        </button>
                    </td>
                    <td class="align-middle">
                        {{ item.number }}
                    </td>
                    <td class="align-middle">
                        {{ item.summary }}
                    </td>
                    <td class="align-middle">
                        <b-form-select v-model="item.assigneeId" :options="users" disabled></b-form-select>
                    </td>
                    <td class="align-middle">
                        <b-form-select v-model="item.reporterId" :options="users" disabled></b-form-select>
                    </td>
                    <td class="align-middle">
                        {{ item.statusId }}
                    </td>
                    <td class="align-middle">
                        <b-form-select v-model="item.createUser" :options="users" disabled></b-form-select>
                    </td>
                    <td class="align-middle">
                        <button type="button" class="btn btn-outline-danger btn-sm" @click.prevent="tempIssue = item; openDeleteModal(item)">
                            <font-awesome-icon icon="trash-alt" />
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

        <!-- Add Issue Modal -->
        <b-modal id="addIssueModal" title="新增問題" @ok="createIssue">
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm">
                        <div class="form-group">
                            <label for="title">問題名稱</label>
                            <input type="text" class="form-control" id="title" placeholder="" required
                                v-model="tempIssue.summary">
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <label for="assigneeId">被指派者</label>
                                <b-form-select v-model="tempIssue.assigneeId" id="assigneeId" :options="users" size="sm" class="mt-3" required></b-form-select>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="reporterId">回報者</label>
                                <b-form-select v-model="tempIssue.reporterId" id="reporterId" :options="users" size="sm" class="mt-3" required></b-form-select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="description">問題描述</label>
                            <textarea type="text" class="form-control" id="description" placeholder=""
                                v-model="tempIssue.description"></textarea>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <label for="statusId">問題狀態</label>
                                <b-form-select v-model="tempIssue.statusId" id="statusId" :options="statusOptions" size="sm" class="mt-3" required></b-form-select>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="kindId">問題類別</label>
                                <b-form-select v-model="tempIssue.kindId" id="kindId" :options="kindOptions" size="sm" class="mt-3" required></b-form-select>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <label for="serverityId">問題嚴重性</label>
                                <b-form-select v-model="tempIssue.serverityId" id="serverityId" :options="serverityOptions" size="sm" class="mt-3" required></b-form-select>
                            </div>
                            <div class="form-group col-md-6">
                                <label for="urgencyId">問題緊急性</label>
                                <b-form-select v-model="tempIssue.urgencyId" id="urgencyId" :options="urgencyOptions" size="sm" class="mt-3" required></b-form-select>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </b-modal>
    </div>
</template>

<script>
    import _ from 'lodash';
    import NavBar from '@/components/NavBar.vue';

    export default {
        name: 'IssueIndex',
        components: {
            NavBar
        },
        data() {
            return {
                searchText: '',
                createModal: false,
                tempIssue: {
                    number: '',
                    summary: '',
                    assigneeId: '',
                    reporterId: '',
                    statusId: '',
                    description: '',
                    kindId: '',
                    serverityId: '',
                    urgencyId: '',
                    projectId: ''
                },
                issues: [],
                statusOptions:[
                    { value: 1, text: '未分配的問題' },
                    { value: 2, text: '已分配未開始的問題' },
                    { value: 3, text: '正在做的問題' },
                    { value: 4, text: '完成後被重新打開的問題' },
                    { value: 5, text: '已完成的問題' },
                    { value: 6, text: '待定的問題' }
                ],
                kindOptions:[
                    { value: 1, text: '疑難排解' },
                    { value: 2, text: '功能開發' },
                    { value: 3, text: '平面設計' },
                    { value: 4, text: '其他' }
                ],
                serverityOptions:[
                    { value: 1, text: '未知' },
                    { value: 2, text: '不重要的' },
                    { value: 3, text: '次要的' },
                    { value: 4, text: '危急的' },
                    { value: 5, text: '重要的' }
                ],
                urgencyOptions:[
                    { value: 1, text: '緊急' },
                    { value: 2, text: '盡速' },
                    { value: 3, text: '普通' },
                    { value: 4, text: '不急' }
                ],
                users:[],
                statusPresent:{ 1: '未分配的問題', 2: '已分配未開始的問題', 3: '正在做的問題', 4: '完成後被重新打開的問題', 5: '已完成的問題', 6: '待定的問題'},
            };
        },
        computed: {
            handleIssues() {
                const vm = this;
                return _.filter(vm.issues, function (issue) {
                    return issue.summary.match(vm.searchText);
                });
            }
        },
        methods: {
            getIssueList() {
                const api = `${process.env.VUE_APP_APIPATH}/api/issue`;
                const vm = this;
                const token = localStorage.getItem('token');
                const id = localStorage.getItem('user_id');
                this.$http.get(
                    api,
                    { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
                ).
                then((response) => {
                    console.log(response)
                    if(response.status == 200){
                        response.data.forEach(function(v, i, arr){
                            if(v.reporterId == id || v.createUser == id || v.assigneeId == id){
                                vm.issues.push(v);
                                vm.issues[vm.issues.length - 1].statusId = vm.statusPresent[vm.issues[vm.issues.length - 1].statusId]
                            }
                        })
                    }
                }); 
            },
            openAddModal() {
                this.$bvModal.show('addIssueModal');
            },
            closeAddModal() {
                this.$bvModal.hide('addIssueModal');
            },
            openDeleteModal(issue) {
                this.$bvModal.msgBoxConfirm('確定刪除?',{
                    okTitle: '刪除',
                    cancelTitle: '取消',
                    centered: true
                })
                .then(value => {
                    if (value){
                        const api = `${process.env.VUE_APP_APIPATH}/api/issue/` + issue.id;
                        const vm = this;
                        const token = localStorage.getItem('token');
                        this.$http.delete(
                            api,
                            { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
                        ).
                        then((response) => {
                            console.log(response)
                            if(response.status == 200){
                                alert('刪除問題成功!');
                                this.$router.go();
                            }
                        }); 
                    }
                })
            },
            clearTempIssue() {
                const emptyIssue = {
                    number: '',
                    summary: '',
                    assigneeId: '',
                    reporterId: '',
                    statusId: '',
                    description: '',
                    kindId: '',
                    serverityId: '',
                    urgencyId: '',
                    projectId: ''
                };
                this.tempIssue = emptyIssue;
            },
            createIssue(){
                const api = `${process.env.VUE_APP_APIPATH}/api/issue`;
                const vm = this;
                const token = localStorage.getItem('token');
                vm.tempIssue.createUser = parseInt(localStorage.getItem('user_id'));
                vm.tempIssue.number = Math.floor(100000 + Math.random() * 900000)
                this.$http.post(
                    api,
                    vm.tempIssue,
                    { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
                ).
                then((response) => {
                    console.log(response)
                    if(response.status == 200){
                       alert('新增問題成功!');
                       this.$router.go();
                    }
                }); 
            },
            checkIssue(issue){
                const vm = this;
                vm.$router.push({ path: `/issue/${issue.id}`})
            },
            getAllUser(){
                const api = `${process.env.VUE_APP_APIPATH}/api/user`;
                const vm = this;
                const token = localStorage.getItem('token');
                this.$http.get(
                    api,
                    { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
                ).
                then((response) => {
                    // console.log(response)
                    if(response.status == 200){
                        response.data.forEach(user => vm.users.push({ value: user.id, text: user.name }));
                    }
                }); 
            },
            handleUser(id) {
                const vm = this;
                this.users.forEach(function(v, i, arr){
                    if(v.value == id){
                        console.log(v.text)
                        return v.text
                    }
                })
            }
        },
        created(){
            this.getIssueList();
            this.getAllUser();
        }
    }
</script>