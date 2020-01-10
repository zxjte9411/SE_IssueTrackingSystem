<template>
    <div>
        <NavBar />
        <div>

        </div>
        <b-container fluid class="pl-5 pr-5 mt-3">
            <b-form>
                <b-form-group id="input-group-issue">
                    <b-row class="pb-3">
                        <b-col cols="3">
                            <label for="input-number">問題單號</label>
                            <b-form-input id="input-number" v-model="issue.number" type="string" required>
                            </b-form-input>
                        </b-col>
                        <b-col cols="3">
                            <label for="input-summary">問題標題</label>
                            <b-form-input id="input-summary" v-model="issue.summary" type="string" required>
                            </b-form-input>
                        </b-col>
                        <b-col cols="3">
                            <label for="input-summary">隸屬專案</label>
                            <b-form-select v-model="issue.projectId" :options="projectOptions" class="mb-3" value-field="id" text-field="name" disabled-field="notEnabled">
                            </b-form-select>
                        </b-col>
                        <b-col cols="3">
                            <label for="input-IssueCreateUser">創立者</label>
                            <b-form-select v-model="issue.createUser" id="input-IssueCreateUser" :options="users" disabled></b-form-select>
                        </b-col>
                    </b-row>
                    <b-row class="pb-3">
                        <b-col>
                            <label for="input-description">問題描述</label>
                            <b-form-textarea id="input-description" v-model="issue.description">
                            </b-form-textarea>
                        </b-col>
                    </b-row>
                    <b-row class="pb-3">
                        <b-col>
                            <label for="input-assigneeId">被指派者</label>
                            <b-form-select v-model="issue.assigneeId" id="input-assigneeId" :options="users" required></b-form-select>
                        </b-col>
                        <b-col>
                            <label for="input-reporterId">回報者</label>
                            <b-form-select v-model="issue.reporterId" id="input-reporterId" :options="users" required></b-form-select>
                        </b-col>
                    </b-row>
                    <b-row class="pb-3">
                        <b-col>
                            <label for="input-statusId">狀態</label>
                            <b-form-select v-model="issue.statusId" id="input-statusId" :options="statusOptions" required></b-form-select>
                        </b-col>
                        <b-col>
                            <label for="input-kindId">種類</label>
                            <b-form-select v-model="issue.kindId" id="input-kindId" :options="kindOptions" required></b-form-select>
                        </b-col>
                    </b-row>
                    <b-row class="pb-3">
                        <b-col>
                            <label for="input-serverityId">重要性</label>
                            <b-form-select v-model="issue.serverityId" id="input-serverityId" :options="serverityOptions" required></b-form-select>
                        </b-col>
                        <b-col>
                            <label for="input-urgencyId">緊急性</label>
                            <b-form-select v-model="issue.urgencyId" id="input-urgencyId" :options="urgencyOptions" required></b-form-select>
                        </b-col>
                    </b-row>
                    <b-row class="pb-3">
                        <b-col>
                            <label for="input-estimatedTime">估計處理時間</label>
                            <b-form-input id="input-estimatedTime" v-model="issue.estimatedTime" type="number"></b-form-input>
                        </b-col>
                        <b-col>
                            <label for="input-estimatedStartTime">估計開始時間</label>
                            <b-form-input id="input-estimatedStartTime" v-model="issue.estimatedStartTime" type="datetime-local"></b-form-input>
                        </b-col>
                        <b-col>
                            <label for="input-estimatedEndTime">估計結束時間</label>
                            <b-form-input id="input-estimatedEndTime" v-model="issue.estimatedEndTime" type="datetime-local"></b-form-input>
                        </b-col>
                    </b-row>
                    <b-row class="pb-3">
                        <b-col>
                            <label for="input-actualStartTime">實際開始時間</label>
                            <b-form-input id="input-actualStartTime" v-model="issue.actualStartTime" type="datetime-local"></b-form-input>
                        </b-col>
                        <b-col>
                            <label for="input-actualEndTime">實際結束時間</label>
                            <b-form-input id="input-actualEndTime" v-model="issue.actualEndTime" type="datetime-local"></b-form-input>
                        </b-col>
                        <b-col>
                            <label for="input-resolveTime">問題解決時間</label>
                            <b-form-input id="input-resolveTime" v-model="issue.resolveTime" type="datetime-local"></b-form-input>
                        </b-col>
                    </b-row>
                </b-form-group>
            </b-form>
        </b-container>
        <b-button variant="secondary" @click="updateIssue">修改</b-button>
    </div>
</template>

<script>
    import _ from 'lodash';
    import NavBar from '@/components/NavBar.vue';

    export default {
        name: 'IssuePage',
        components: {
            NavBar
        },
        data() {
            return {
                issue: {
                    number: '',
                    summary: '',
                    description: '',
                    assigneeId: '',
                    reporterId: '',
                    estimatedTime: 0,
                    estimatedStartTime: '',
                    estimatedEndTime: '',
                    actualStartTime: '',
                    actualEndTime: '',
                    resolveTime: '',
                    kindId: '',
                    serverityId: '',
                    statusId: '',
                    urgencyId: '',
                    createTime: '',
                    createUser: '',
                    modifyTime: '',
                    ModifyUser: '',
                    projectId: ''
                },
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
                users: [],
                projectOptions: []
            };
        },
        methods: {
            getIssue() {
                const vm = this;
                const api = 'http://lspssapple.asuscomm.com:81/api/issue/' + vm.$route.params.id;
                const token = localStorage.getItem('token');
                this.$http.get(
                    api,
                    { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
                ).
                then((response) => {
                    console.log(response)
                    if(response.status == 200){
                        vm.issue = response.data;
                    }
                }); 
            },
            getAllProject(){
                const api = 'http://lspssapple.asuscomm.com:81/api/project';
                const vm = this;
                const token = localStorage.getItem('token');
                this.$http.get(
                    api,
                    { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
                ).
                then((response) => {
                    // console.log(response)
                    if(response.status == 200){
                        response.data.forEach(project => vm.projectOptions.push({ id: project.id, name: project.name }));
                    }
                }); 
            },
            getAllUser(){
                const api = 'http://lspssapple.asuscomm.com:81/api/user';
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
            updateIssue(){
                const vm = this;
                const api = 'http://lspssapple.asuscomm.com:81/api/issue/' + vm.issue.id;
                const token = localStorage.getItem('token');
                this.$http.post(
                    api,
                    vm.issue,
                    { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
                ).
                then((response) => {
                    if(response.status == 200){
                        alert('修改成功');
                        vm.$route.go()
                    }
                }); 

            }
        },
        created() {
            this.getIssue();
            this.getAllUser();
            this.getAllProject()
        }
    }
</script>