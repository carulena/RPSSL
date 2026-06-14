
export const styles = {
    root:   { display: "flex", flexDirection: "column", alignItems: "center", gap: 3, py: 4 },
    arena:  { display: "flex", alignItems: "center", justifyContent: "center", gap: 6, width: "100%" },
    fighter:{ display: "flex", flexDirection: "column", alignItems: "center", gap: 1 },
    emoji:  { fontSize: 150, lineHeight: 1, display: "block" },
    label:  { fontSize: 25, color: "#888" },
};

export const css = `
  @keyframes slideInLeft  { from { transform: translateX(-140px); opacity: 0; } to { transform: translateX(0); opacity: 1; } }
  @keyframes slideInRight { from { transform: translateX(140px);  opacity: 0; } to { transform: translateX(0); opacity: 1; } }
  @keyframes smashLeft  { 0%{transform:translateX(0)} 50%{transform:translateX(28px)} 100%{transform:translateX(0)} }
  @keyframes smashRight { 0%{transform:translateX(0)} 50%{transform:translateX(-28px)} 100%{transform:translateX(0)} }
  @keyframes fallDown   { 0%{transform:translateY(0) rotate(0deg);opacity:1} 100%{transform:translateY(60px) rotate(90deg);opacity:0} }
  @keyframes fadeUp     { from{opacity:0;transform:translateY(8px)} to{opacity:1;transform:translateY(0)} }

  .combat-badge { font-size: 22px; font-weight: 500; padding: 6px 24px; border-radius: 12px; display: inline-block; }
  .combat-badge--win  { background: #e6f4ea; color: #1e7e34; }
  .combat-badge--lose { background: #fce8e6; color: #c62828; }
  .combat-badge--draw { background: #f1f3f4; color: #666; }
`;