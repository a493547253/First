/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using XLua;

public class Helloworld : MonoBehaviour {
    // Use this for initialization
    //void Start () {
    //       LuaEnv luaenv = new LuaEnv();
    //       //luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");

    //       luaenv.DoString("local object = CS.UnityEngine.GameObject('hello world')");
    //       GameObject obj = new GameObject();
    //       luaenv.Dispose();
    //}

    //// Update is called once per frame
    //void Update () {

    //}

    public TextAsset m_luaAssets;
    private System.Action luaStart;
    private System.Action luaUpdate;

    private static LuaEnv m_luaEnv = new LuaEnv();
    private LuaTable m_scriptEnv;

    private void Awake()
    {
        m_scriptEnv = m_luaEnv.NewTable();
        LuaTable meta = m_luaEnv.NewTable();
        meta.Set("__index", m_luaEnv.Global);
        m_scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        m_scriptEnv.Set("self", this);

        m_luaEnv.DoString(m_luaAssets.text, "LuaBehaviour", m_scriptEnv);

        m_scriptEnv.Get("onStart", out luaStart);
        m_scriptEnv.Get("onUpdate", out luaUpdate);

    }

    private void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    private void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
    }

}
