"use client"
import {
	BarElement,
	CategoryScale,
	Chart as ChartJS,
	Legend,
	LinearScale,
	Title,
	Tooltip,
} from "chart.js";

ChartJS.register(
	CategoryScale,
	LinearScale,
	BarElement,
	Title,
	Tooltip,
	Legend,
);
import { useEffect, useState } from "react";
import { Bar } from "react-chartjs-2";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import nextConfig from "../../next.config";
const BASE_PATH = nextConfig.basePath || "";

const Home = () => {
	const [selectedDate, setSelectedDate] = useState<Date | null>(null);
	const [chartData, setChartData] = useState<any>(null);
	const [originalData, setOriginalData] = useState<any[]>([]);

	// 初期データを読み込む
	useEffect(() => {
		fetch(`${BASE_PATH}/data.json`)
			.then((res) => res.json())
			.then((data) => {
				setOriginalData(data); // 元データを保存
				setChartData(transformData(data)); // 初期表示用
			});
	}, []);

	// 日付選択時のフィルタリング処理
	useEffect(() => {
		if (selectedDate && originalData.length > 0) {
			const filteredData = originalData.filter((d: any) => {
				const dataDate = new Date(d.date);
				return dataDate.toDateString() === selectedDate.toDateString();
			});

			setChartData(transformData(filteredData));
		} else {
			setChartData(transformData(originalData)); // 全データを表示
		}
	}, [selectedDate, originalData]);

	// グラフ用データ変換
	const transformData = (data: any[]) => {
		const labels = data.map((d: any) => d.date);
		const successData = data.map((d: any) => d.success);
		const errorData = data.map((d: any) => d.error);

		return {
			labels,
			datasets: [
				{
					label: "Success",
					data: successData,
					backgroundColor: "rgba(75,192,192,0.6)",
				},
				{
					label: "Error",
					data: errorData,
					backgroundColor: "rgba(255,99,132,0.6)",
				},
			],
		};
	};

	return (
		<div>
			<h1>Dynamic Graph with Date Picker</h1>

			{/* 日付選択ピッカー */}
			<div>
				<label>日付を選択:</label>
				<DatePicker
					selected={selectedDate}
					onChange={(date: Date) => setSelectedDate(date)}
					dateFormat="yyyy-MM-dd"
					isClearable
				/>
			</div>

			{/* グラフ表示 */}
			{chartData ? (
				<Bar
					data={chartData}
					options={{
						responsive: true,
						scales: {
							y: { beginAtZero: true },
						},
					}}
				/>
			) : (
				<p>Loading...</p>
			)}
		</div>
	);
};

export default Home;
