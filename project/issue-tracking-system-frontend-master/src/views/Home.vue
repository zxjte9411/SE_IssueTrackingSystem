<template>
  <div class="home">
    <NavBar />
    <div>
      {{getkindTable}}
      {{getserverityTable}}
      {{getuserTable}}
    </div>
    <b-container fluid class="mt-3 bv-example-row bv-example-row-flex-cols">
      <b-row>
        <b-col cols="1">
          <h3>Dashboard</h3>
        </b-col>
        <b-col />
      </b-row>

      <b-row class="mt-3">
        <b-col></b-col>
        <b-col cols="5">
          <b-alert show variant="info">我負責的Issue</b-alert>
          <b-table sticky-header head-variant="light" hover :items="IssueItems" :fields="IssueFields"></b-table>
        </b-col>
        <b-col></b-col>
        <b-col cols="5">
          <b-alert show variant="info">Issue類別</b-alert>
          <b-table head-variant="light" hover :items="KindItems[1]" :fields="KindFields"></b-table>
        </b-col>
        <b-col></b-col>
      </b-row>

      <b-row class="mt-3">
        <b-col></b-col>
        <b-col cols="5">
          <b-alert show variant="info">重要性與緊急性</b-alert>
          <b-table head-variant="light" hover :items="ServerityItems[1]" :fields="ServerityFields"></b-table>
        </b-col>
        <b-col></b-col>
        <b-col cols="5">
          <b-alert show variant="info">其餘人員負責</b-alert>
          <b-table sticky-header head-variant="light" hover :items="UserItems[1]" :fields="UserFields"></b-table>
        </b-col>
        <b-col></b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
  import NavBar from '@/components/NavBar.vue';

  export default {
    name: 'home',
    components: {
      NavBar
    },
    data() {
      return {
            issues: [],
            users: [],
            IssueFields: [
              { key: "number", sortable: true },
              { key: "status", sortable: true },
              { key: "summary", sortable: false }
            ],
            KindFields: [
              { key: "kind", sortable: true },
              { key: "quantity", sortable: true },
              { key: "percentage", sortable: true }
            ],
            ServerityFields: [
              { key: "urgency", sortable: true },
              { key: "unknown", ortable: true },
              { key: "trivial", sortable: true },
              { key: "minor", sortable: true },
              { key: "critical", sortable: true },
              { key: "major", sortable: true }
            ],
            UserFields: [
              { key: "assignee", sortable: true },
              { key: "backlog", sortable: true },
              { key: "open", sortable: true },
              { key: "inProgess", sortable: true },
              { key: "reopened", sortable: true },
              { key: "resolved", sortable: true },
              { key: "pending", sortable: true }
            ],
            IssueItems: [],
            KindItems: [],
            ServerityItems: [],
            UserItems: [],
            statusPresent:{ 1: '未分配的問題', 2: '已分配未開始的問題', 3: '正在做的問題', 4: '完成後被重新打開的問題', 5: '已完成的問題', 6: '待定的問題'},
            kindQuantity: [0,0,0,0,0],
            kindPercentage: ['','','','',''],
            urgentQuantity: [0,0,0,0,0,0],
            asFastAsPossibleQuantity: [0,0,0,0,0,0],
            normalQuantity: [0,0,0,0,0,0],
            notUrgentQuantity: [0,0,0,0,0,0],
            totalQuantity: [0,0,0,0,0,0]
        };
    },
    computed: {
        getkindTable: function () {
          console.log("runKindTable")
          this.issues.forEach(element => {
            if(element.kind == 1){this.kindQuantity[1]=this.kindQuantity[1]+1}
            else if(element.kind == 2){this.kindQuantity[2]+=1}
            else if(element.kind == 3){this.kindQuantity[3]+=1}
            else if(element.kind == 4){this.kindQuantity[4]+=1}
            else{this.kindQuantity[0]+=1}
          });
          const total = this.kindQuantity[1]+this.kindQuantity[2]+this.kindQuantity[3]+this.kindQuantity[4];
          this.kindPercentage[1]=(((this.kindQuantity[1]/total) * 100).toFixed(1) + '%');
          this.kindPercentage[2]=(((this.kindQuantity[2]/total) * 100).toFixed(1) + '%');
          this.kindPercentage[3]=(((this.kindQuantity[3]/total) * 100).toFixed(1) + '%');
          this.kindPercentage[4]=(((this.kindQuantity[4]/total) * 100).toFixed(1) + '%');
          this.KindItems.push([
            { kind: "疑難排解", quantity: this.kindQuantity[1], percentage: this.kindPercentage[1]},
            { kind: "功能開發", quantity: this.kindQuantity[2], percentage: this.kindPercentage[2]},
            { kind: "平面設計", quantity: this.kindQuantity[3], percentage: this.kindPercentage[3]},
            { kind: "其他", quantity: this.kindQuantity[4], percentage: this.kindPercentage[4]}]);
          return ""
        },
        getserverityTable: function () {
          console.log("runServerityTable")
          this.issues.forEach(element => {
            if(element.urgency == 1){
              if(element.serverity==1){this.urgentQuantity[1]+=1;this.totalQuantity[1]+=1}
              else if(element.serverity==2){this.urgentQuantity[2]+=1;this.totalQuantity[2]+=1}
              else if(element.serverity==3){this.urgentQuantity[3]+=1;this.totalQuantity[3]+=1}
              else if(element.serverity==4){this.urgentQuantity[4]+=1;this.totalQuantity[4]+=1}
              else if(element.serverity==5){this.urgentQuantity[5]+=1;this.totalQuantity[5]+=1}}
            else if(element.urgency == 2){
              if(element.serverity==1){this.asFastAsPossibleQuantity[1]+=1;this.totalQuantity[1]+=1}
              else if(element.serverity==2){this.asFastAsPossibleQuantity[2]+=1;this.totalQuantity[2]+=1}
              else if(element.serverity==3){this.asFastAsPossibleQuantity[3]+=1;this.totalQuantity[3]+=1}
              else if(element.serverity==4){this.asFastAsPossibleQuantity[4]+=1;this.totalQuantity[4]+=1}
              else if(element.serverity==5){this.asFastAsPossibleQuantity[5]+=1;this.totalQuantity[5]+=1}}
            else if(element.urgency == 3){
              if(element.serverity==1){this.normalQuantity[1]+=1;this.totalQuantity[1]+=1}
              else if(element.serverity==2){this.normalQuantity[2]+=1;this.totalQuantity[2]+=1}
              else if(element.serverity==3){this.normalQuantity[3]+=1;this.totalQuantity[3]+=1}
              else if(element.serverity==4){this.normalQuantity[4]+=1;this.totalQuantity[4]+=1}
              else if(element.serverity==5){this.normalQuantity[5]+=1;this.totalQuantity[5]+=1}}
            else if(element.urgency == 4){
              if(element.serverity==1){this.notUrgentQuantity[1]+=1;this.totalQuantity[1]+=1}
              else if(element.serverity==2){this.notUrgentQuantity[2]+=1;this.totalQuantity[2]+=1}
              else if(element.serverity==3){this.notUrgentQuantity[3]+=1;this.totalQuantity[3]+=1}
              else if(element.serverity==4){this.notUrgentQuantity[4]+=1;this.totalQuantity[4]+=1}
              else if(element.serverity==5){this.notUrgentQuantity[5]+=1;this.totalQuantity[5]+=1}}
          });
          this.ServerityItems.push([
            { urgency: "緊急", unknown: this.urgentQuantity[1], trivial: this.urgentQuantity[2], minor: this.urgentQuantity[3], critical: this.urgentQuantity[4], major: this.urgentQuantity[5] },
            { urgency: "盡速", unknown: this.asFastAsPossibleQuantity[1], trivial: this.asFastAsPossibleQuantity[1], minor: this.asFastAsPossibleQuantity[4], critical: this.asFastAsPossibleQuantity[4], major: this.asFastAsPossibleQuantity[5] },
            { urgency: "普通", unknown: this.normalQuantity[1], trivial: this.normalQuantity[2], minor: this.normalQuantity[3], critical: this.normalQuantity[4], major: this.normalQuantity[5] },
            { urgency: "不急", unknown: this.notUrgentQuantity[1], trivial: this.notUrgentQuantity[1], minor: this.notUrgentQuantity[3], critical: this.notUrgentQuantity[3], major: this.notUrgentQuantity[5] },
            { urgency: "Total", unknown: this.totalQuantity[1], trivial: this.totalQuantity[2], minor: this.totalQuantity[3], critical: this.totalQuantity[4], major: this.totalQuantity[5] }
          ]);
          return ""
        },
        getuserTable: function () {
          console.log("runUserTable")
          let userissue = [0,0,0,0,0,0,0]
          let info = {}
          let infoclone = {}
          let temp = []
          this.users.forEach(user => {
            this.issues.forEach(issue => {
              if(issue.assignee == user.value){
                if(issue.status==1){userissue[1]+=1}
                else if(issue.status==2){userissue[2]+=1}
                else if(issue.status==3){userissue[3]+=1}
                else if(issue.status==4){userissue[4]+=1}
                else if(issue.status==5){userissue[5]+=1}
                else if(issue.status==6){userissue[6]+=1}
              }
            })
            info = {assignee: user.text, backlog: userissue[1], open: userissue[2], inProgess: userissue[3], reopened: userissue[4], resolved: userissue[5], pending: userissue[6] }
            infoclone = Object.assign({}, info)
            temp.push(infoclone)
            info = {}
            userissue = userissue = [0,0,0,0,0,0,0]
          })
          this.UserItems.push(temp)
        }
    },
    methods: {
        getIssueList() {
            const api = `${process.env.VUE_APP_APIPATH}/api/issue`;
            const vm = this;
            const token = localStorage.getItem('token');
            const id = localStorage.getItem('userId');
            this.$http.get(
                api,
                { headers: { "Authorization": "Bearer " + token, "content-type": "application/json;charset=utf-8"}}
            ).
            then((response) => {
                // console.log(response)
                if(response.status == 200){
                  response.data.forEach(issue => vm.issues.push({ number: issue.number, assignee: issue.assigneeId, status: issue.statusId, summary: issue.summary, serverity: issue.serverityId, urgency: issue.urgencyId , kind: issue.kindId }));
                  response.data.forEach(function(v, i, arr){
                    if(v.assigneeId == id){
                        vm.IssueItems.push({ number: v.number, status: v.statusId, summary: v.summary });
                        vm.IssueItems[vm.IssueItems.length - 1].status = vm.statusPresent[vm.IssueItems[vm.IssueItems.length - 1].status]
                    }
                  })
                }
            }); 
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
                    response.data.forEach(user => vm.users.push({ value: user.id, text: user.account }));
                }
            }); 
        }
    },
    created() {
        this.getIssueList();
        this.getAllUser();
    }
  }
</script>
