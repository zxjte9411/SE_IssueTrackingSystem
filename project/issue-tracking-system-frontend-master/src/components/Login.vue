<template>
  <div>
    <b-form @submit.prevent="login">
      <b-form-group
        id="fieldset-userID"
        label="Enter your userID"
        label-for="input-userID"
        :invalid-feedback="invalidUserIDFeedback"
        :valid-feedback="validFeedback"
        :state="userIDState"
      >
        <b-form-input id="account" v-model="userID" :state="userIDState" type="text" trim required></b-form-input>
      </b-form-group>

      <b-form-group
        id="fieldset-password"
        label="Enter your password"
        label-for="input-password"
        :invalid-feedback="invalidPasswordFeedback"
        :valid-feedback="validFeedback"
        :state="passwordState"
      >
        <b-form-input
          id="password"
          v-model="password"
          :state="passwordState"
          type="password"
          trim
          required
        ></b-form-input>
      </b-form-group>
      <b-button type="submit" :disabled="!btnLoginEnabled">Login In</b-button>
      <b-modal id="Alertmodal" ref="AlertModal" ok-only centered>
        <b-alert class="left" show variant="danger">"wrong account or password"</b-alert>
      </b-modal>
    </b-form>
  </div>
</template>
<script>
import { async } from "q";
const axios = require("axios").default;

export default {
  data() {
    return {
      userID: "",
      password: "",
      isPasswordOrUserIDError: false
    };
  },
  components: {},
  methods: {
    async login() {
      // write login authencation logic here!
        let res = await axios
          .post(
            `${process.env.VUE_APP_APIPATH}/api/user/token`,
            { account: this.userID, password: this.password },
            { headers: { "content-type": "application/json;charset=utf-8" } }
          )
          .then(async function(res) {
            return res;
          })
          .catch(async function(err) {
            console.log(err.response);
            return err;
          });
        if (res.status >= 200 && res.status < 300) {
          localStorage.setItem("token", res.data.token);
          localStorage.setItem("user_id", res.data.userId);
          this.isPasswordOrUserIDError = false;
          this.$router.push("/");
        } else {
          this.isPasswordOrUserIDError = true;
          this.$refs.AlertModal.show();
        }
    }
  },
  computed: {
    btnLoginEnabled() {
      return (
        this.userIDState && this.passwordState && !this.isPasswordOrUserIDError
      );
    },
    userIDState() {
      return this.userID.length >= 4 && !this.isPasswordOrUserIDError;
    },
    passwordState() {
      return this.password.length >= 4 && !this.isPasswordOrUserIDError;
    },
    invalidUserIDFeedback() {
      if (this.userID.length >= 4) {
        this.isPasswordOrUserIDError = false;
        return "";
      } else if (this.userID.length > 0) {
        this.isPasswordOrUserIDError = true;
        return "Enter at least 4 characters";
      } else {
        this.isPasswordOrUserIDError = true;
        return "Please enter something";
      }
    },
    invalidPasswordFeedback() {
      if (this.password.length >= 4) {
        this.isPasswordOrUserIDError = false;
        return "";
      } else if (this.password.length > 0) {
        this.isPasswordOrUserIDError = true;
        return "Enter at least 4 characters";
      } else {
        this.isPasswordOrUserIDError = true;
        return "Please enter something";
      }
    },
    validFeedback() {
      // return this.state === true ? "Thank you" : "";
    }
  }
};
</script>
