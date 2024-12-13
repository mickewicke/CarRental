import React, { useState } from "react";
import {
  DesktopOutlined,
  FileOutlined,
  PieChartOutlined,
  TeamOutlined,
  CarOutlined,
} from "@ant-design/icons";
import type { MenuProps } from "antd";
import {
  Layout,
  Menu,
  theme,
  Typography,
Steps,
  Card,
  Space,
  Image,
  Row,
  Col,
} from "antd";
import { Pickup } from "./PickupFlow/PickupMain";
const { Title } = Typography;
const { Header, Content, Footer, Sider } = Layout;

type MenuItem = Required<MenuProps>["items"][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,

  children?: MenuItem[]
): MenuItem {
  return {
    key,

    children,
    label,
  };
}

const items: MenuItem[] = [


  getItem("Pickup", "1"),
  getItem("Return", "2"),
];

const App: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  return (
    <Layout style={{ minHeight: "100vh", minWidth: "800px"}}>
      <Sider
        
        collapsed={collapsed}
        onCollapse={(value) => setCollapsed(value)}
      >
        <div className="demo-logo-vertical" />
        <Menu
          theme="dark"
          defaultSelectedKeys={["1"]}
          mode="inline"
          items={items}
        />
      </Sider>
      <Layout>
        <Content style={{ margin: "16px 20px" }}>
        <Pickup/>
        </Content>
        <Footer style={{ textAlign: "center" }}>
          
        </Footer>
      </Layout>
    </Layout>
  );
};




export default App;
