import type { NextConfig } from "next";

const nextConfig: NextConfig = {
	/* config options here */
	output: "export", // 静的サイトエクスポートを有効化
	basePath: "/jsonl-sample", // GitHub Pages用 (リポジトリ名に合わせて変更)
};

export default nextConfig;
