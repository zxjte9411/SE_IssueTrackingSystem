<template>
  <div class="about">
    <NavBar/>
    <b-container class="bv-example-row mt-5">
    <b-row>
      <b-col>
        <ve-pie :data="statusChartData"></ve-pie>
      </b-col>
      <b-col>
        <ve-pie :data="kindChartData"></ve-pie>
      </b-col>
    </b-row>
    <b-row>
      <b-col>  
        <ve-pie :data="serverityChartData"></ve-pie>
      </b-col>
      <b-col>
        <ve-pie :data="urgencyChartData"></ve-pie>
      </b-col>
    </b-row>
  </b-container>
  </div>
</template>
<script>

import NavBar from "@/components/NavBar.vue";

export default {
  name: 'report',
  components: {
    NavBar
  },
  data(){
    return{
      issues: [],
      statusChartData: {
        columns: ['status', 'count'],
        rows: [
          { 'status': '未分配的問題', 'count': 0 },
          { 'status': '已分配未開始的問題', 'count': 0 },
          { 'status': '正在做的問題', 'count': 0 },
          { 'status': '完成後被重新打開的問題', 'count': 0 },
          { 'status': '已完成的問題', 'count': 0 },
          { 'status': '待定的問題', 'count': 0 },
        ]
      },
      kindChartData: {
        columns: ['status', 'count'],
        rows: [
          { 'status': '疑難排解', 'count': 0 },
          { 'status': '功能開發', 'count': 0 },
          { 'status': '平面設計', 'count': 0 },
          { 'status': '其他', 'count': 0 }
        ]
      },
      serverityChartData: {
        columns: ['status', 'count'],
        rows: [
          { 'status': '未知', 'count': 0 },
          { 'status': '不重要的', 'count': 0 },
          { 'status': '次要的', 'count': 0 },
          { 'status': '危急的', 'count': 0 },
          { 'status': '重要的', 'count': 0 }
        ]
      },
      urgencyChartData: {
        columns: ['status', 'count'],
        rows: [
          { 'status': '緊急', 'count': 0 },
          { 'status': '盡速', 'count': 0 },
          { 'status': '普通', 'count': 0 },
          { 'status': '不急', 'count': 0 }
        ]
      },
    }
  },
  methods: {
    getAllIssue() {
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
          vm.issues = response.data
          response.data.forEach(function(v, i, arr){
            vm.statusChartData.rows[v.statusId - 1].count += 1;
            vm.kindChartData.rows[v.kindId - 1].count += 1;
            vm.serverityChartData.rows[v.serverityId - 1].count += 1;
            vm.urgencyChartData.rows[v.urgencyId - 1].count += 1;
          })
        }
      }); 
    },
  },
  created() {
    this.getAllIssue();
  }
}
</script>