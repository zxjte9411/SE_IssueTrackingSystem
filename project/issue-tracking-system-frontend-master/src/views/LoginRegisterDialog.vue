<template>
  <div>
    <NavBar />
    <b-modal id="modal-scoped" centered hide-footer no-close-on-backdrop no-close-on-esc :title="titleName[tabIndex]">
      <b-tabs content-class="mt-3" v-model="tabIndex" lazy>
        <b-tab title="Login">
          <Login/>
        </b-tab>
        <b-tab title="Register" >
          <Register @registerSuccess="tabIndex=0"/>
        </b-tab>
      </b-tabs>
    </b-modal>
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";
import Login from "@/components/Login.vue";
import Register from "@/components/Register.vue";

export default {
  name: "LoginRegisterDialog",
  mounted() {
    this.$bvModal.show("modal-scoped");
    this.tabIndex = 0
  },
  data() {
    return {
      titleName: ["Login", "Register"],
      tabIndex: 0
    };
  },
  methods: {
    linkClass(idx) {
      if (this.tabIndex === idx) {
        return ["bg-primary", "text-light"];
      } else {
        return ["bg-light", "text-info"];
      }
    },
  },
  components: {
    NavBar,
    Login,
    Register
  }
};
</script>