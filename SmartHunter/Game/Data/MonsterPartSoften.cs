using System.ComponentModel;
using System.Linq;
using SmartHunter.Core.Data;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data
{
    public class MonsterPartSoften : TimedVisibility
    {
        Monster m_Owner;
        public ulong Address { get; private set; }

        public Progress Time { get; private set; }

        uint m_PartID;
        public uint PartID
        {
            get { return m_PartID; }
            set { SetProperty(ref m_PartID, value); }
        }

        uint m_TimesBrokenCount;
        public uint TimesBrokenCount
        {
            get { return m_TimesBrokenCount; }
            set { SetProperty(ref m_TimesBrokenCount, value); }
        }

        public string Name
        {
            get
            {
                //return LocalizationHelper.GetMonsterPartName(m_Owner.Id, m_Owner.Parts.Where(part => part.IsRemovable == IsRemovable).ToList().IndexOf(this), IsRemovable);
                //return Address + ":PartID:" + PartID + "Count:" + TimesBrokenCount;
                return LocalizationHelper.GetMonsterSoftenPartName(m_Owner.Id, PartID);
            }
        }

        public string GroupId
        {
            get
            {
                //return GetGroupIdFromIndex(m_Owner.Id, m_Owner.Parts.Where(part => part.IsRemovable == IsRemovable).ToList().IndexOf(this), IsRemovable);
                return Address + ":PartID:" + PartID + "Count:" + TimesBrokenCount;
            }
        }

        public bool IsVisible
        {
            get
            {
                return IsIncluded(GroupId) && IsTimeVisible(false, ConfigHelper.Main.Values.Overlay.MonsterWidget.HideSoftenPartsAfterSeconds) && ConfigHelper.Main.Values.Overlay.MonsterWidget.ShowSoftenParts;
            }
        }

        public MonsterPartSoften(Monster owner, ulong address, float maxTime, float currentTime, uint timesBrokenCount, uint partID)
        {
            m_Owner = owner;
            Address = address;
            Time = new Progress(maxTime, currentTime);
            PartID = partID;
            m_TimesBrokenCount = timesBrokenCount;

            PropertyChanged += MonsterPartSoften_PropertyChanged;
            Time.PropertyChanged += Time_PropertyChanged;
        }

        private void MonsterPartSoften_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PartID))
            {
                UpdateLastChangedTime();
            }
        }

        private void Time_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateLastChangedTime();
        }

        public static string GetGroupIdFromIndex(string monsterId, int index)
        {
            return string.Format("{0}:{1}", monsterId, index);
        }

        public static bool IsIncluded(string groupId)
        {
            return ConfigHelper.Main.Values.Overlay.MonsterWidget.MatchIncludePartGroupIdRegex(groupId);
        }
    }
}
