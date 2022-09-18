<template>
    <h1>用戶資訊</h1>
    <div>您的權限 : {{getPermission()}}</div>
    <div>{{getMsg()}}</div>
    <div class="tableDiv">
        <table class="table">
            <thead>
                <td>工單編號</td>
                <td>工單訊息</td>
            </thead>
            <tbody>
                <tr v-for="item in result" :key="item.logMsg">
                    <td>{{ item.id }}</td>
                    <td>{{ item.logMsg }}</td>
                </tr>
            </tbody>
        </table>
        <button v-on:click="click()">{{getPermission()}}</button>

        <div v-if="isPopup" class="vue-popUpSmall" :style="popPosition">
            <slot/>
            <slot name="footer">
                <p style="font-size:25px;">{{getPermission()}}</p>
                <div v-if="inputId" >工單編號 : <input type="text" placeholder="請輸入工單編號" v-model="id" /></div>
                <div v-if="inputMsg" >工單訊息 : <input type="text" placeholder="請輸入工單訊息" v-model="msg" /></div>
                <div class="text-center">
                <div class="button">
                    <button v-on:click="send();">送出</button>
                </div>
                <div class="button">
                    <button v-on:click="cancel">取消</button>
                </div>
                </div>
            </slot>
        </div>
    </div>
  </template>

  <script lang="ts">
    import { defineComponent } from '@vue/runtime-core';
    import axios from 'axios';
  
    export default defineComponent({
        name: 'App',
        props: {
            value: {
                type: [Number, String]
            },
             position: {
                type: String,
                default: '0'
            }
        },
        data(){
            return{
                result:[
                    {
                        id:"",
                        logMsg:"",
                    }
                ],
                permission :"",
                inputId:false,
                inputMsg:false,
                isPopup: false,
                id:"",
                msg:""
            }
        },
        computed: {
            popPosition() {
                return `margin-left:${this.position}`
            }
        },
        methods: {
            getPermission() {
                switch(this.$route.query.permission){
                    case "create":
                        this.inputId = false;
                        this.inputMsg = true;
                        return  "新增工單";
                    case "edit":
                        this.inputId = true;
                        this.inputMsg = true;
                        return  "編輯工單";
                    case "delete":
                        this.inputId = true;
                        this.inputMsg = false;
                        return "刪除工單";
                }
            },
            getMsg(){
                axios.get('https://localhost:7185/api/TestLog')
                    .then(response => {
                        this.result = response.data;   
                        
                    }).catch( (error) => console.log(error));
            },
            send(){
                switch(this.$route.query.permission){
                    case "create":
                        axios.post('https://localhost:7185/api/TestLog',{logMsg:this.msg})
                            .then((response) => {
                                this.isPopup = false;
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                    case "edit":
                        axios.put('https://localhost:7185/api/TestLog/'+this.id.trim(),{
                            id:this.id.trim(),
                            logMsg:this.msg
                        })
                            .then((response) => {
                                this.isPopup = false;
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                    case "delete":
                        axios.delete('https://localhost:7185/api/TestLog/'+this.id.trim())
                            .then((response) => {
                                this.isPopup = false;
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                }
                
            },


            close() {
                this.isPopup = false
            },
            click() {
                this.isPopup = !this.isPopup
            },
            cancel() {
                this.isPopup = false
            }
        }
    });
    
  </script>
  
  <style>
    .tableDiv{
        text-align: center;
    }
    .table{
        margin-left:43.5%;
        padding: 1%;
    }
    td{
        border:1px solid black;
    }



    .vue-popUpSmall {
        background-color: #e6e6e6;
        border: 4px solid #b9b9b9;
        color: #000000;
        padding: 5px;
        z-index: 1;
    }

    .vue-input {
        background-color: #e6e6e6;
        border: 4px solid #b9b9b9;
        color: #000000;
        padding: 5px;
        z-index: 1;
    }

    .button {
    display: inline;
    margin: 2px;
    }
  </style>
  