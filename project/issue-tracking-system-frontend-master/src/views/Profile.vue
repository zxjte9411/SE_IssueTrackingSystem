<template>
  <div>
    <NavBar />
    <b-container class="mt-5 bv-example-row">
      <b-card>
        <b-row>
          <b-col cols="3">
            <label for="input-name">名稱</label>
            <b-form-input id="input-name" v-model="user.name" type="text" required></b-form-input>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="3">
            <label for="input-account">帳號</label>
            <b-form-input id="input-account" v-model="user.account" type="text" required></b-form-input>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="3">
            <label for="input-eMail">信箱</label>
            <b-form-input id="input-eMail" v-model="user.eMail" type="email" required></b-form-input>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="3">
            <label for="input-charactorId">身分</label>
            <b-form-input id="input-charactorId" :value="GetCharactorName" type="text" disabled></b-form-input>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="3">
            <label for="input-linwId">Line Id</label>
            <b-form-input id="input-linwId" :value="GetLineid" type="text" disabled></b-form-input>
          </b-col>
        </b-row>
        <b-button variant="secondary" @click="update">修改</b-button>
      </b-card>
    </b-container>
  </div>
</template>
//disabled
<script>
import NavBar from "@/components/NavBar.vue";
import axios from "axios";

export default {
  data() {
    return {
      user: {},
    };
  },
  components: { NavBar },
  methods: {
    async GetUser() {
      const user_id = localStorage.getItem("user_id");
      const token = localStorage.getItem("token");
      const api = `${process.env.VUE_APP_APIPATH}/api/user/${user_id}`;
      const vm = this;
      let res = await axios
        .get(api, {
          headers: {
            Authorization: `Bearer ${token}`,
            "content-type": "application/json;charset=utf-8"
          }
        })
        .then(res => {
          return res;
        })
        .catch(err => {
          return err.response;
        });
      this.user = res.data;
    },
    update() {
      const vm = this;
      const user_id = localStorage.getItem("user_id");
      const token = localStorage.getItem("token");
      const api = `${process.env.VUE_APP_APIPATH}/api/user/${user_id}`;
      axios
        .post(api, vm.user, {
          headers: {
            Authorization: "Bearer " + token,
            "content-type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          if (response.status == 200) {
            alert("修改成功");
            this.$router.go();
          }
        });
    }
  },
  computed:{
    GetLineid(){
      const lineid = this.user.lineId
      if (lineid === null)
        return "尚未設定"
      return lineid
    },
    GetCharactorName(){
       const charactorOptions = [
        { /*charactorId: 0,*/ name: "未配置" },
        { /*charactorId: 1,*/ name: "系統管理員" },
        { /*charactorId: 2,*/ name: "一般使用者" }
      ]
      console.log(this.user.charactorId)
      const CharactorName = charactorOptions[this.user.charactorId].name
      return CharactorName
    }
  },
  mounted() {
    this.GetUser();
  }
};
</script>