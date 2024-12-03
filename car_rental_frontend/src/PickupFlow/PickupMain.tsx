import React, { useEffect, useState } from "react";
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
import { SelectCar } from "./Steps/SelectCar";
import { CarStatus } from "./Steps/CarStatus";
import { CustomerData } from "./Steps/CustomerData";
const { Title } = Typography;
const { Header, Content, Footer, Sider } = Layout;

const steps = [SelectCar, CarStatus, CustomerData, CarStatus];
export function Pickup() {
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const [step, setStep] = useState<number>(0);

  const CurrentStepComponent = steps[step];

  return (
    <>
      <div style={{ margin: "30px 40px", maxWidth: "800px" }}>
        <Title>Register Pickup</Title> <Typography></Typography>
        <Steps
          style={{
            marginTop: "40px",
          }}
          current={step}
          status="process"
          items={[
            {
              title: "Select Car",
            },
            {
              title: "Car Status",
            },
            {
              title: "Customer Data",
            },
            {
              title: "Finalize",
            },
          ]}
        />
      </div>

      <div
        style={{
          marginTop: "40px",
          minHeight: 360,
          maxWidth: "1000px",
        }}
      >
        <CurrentStepComponent />
      </div>
    </>
  );
}

export interface Car {
  VehicleID: number;
  RegistrationNumber: string;
  TypeName: string;
  imageUrl: string;
}
